using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoloVR
{
   
    public class NoloHandleRay : MonoBehaviour
    {
        public enum handRay
        {
            rayLine,
            hand2d,
            hand3d
        }
        public handRay _handRay = handRay.rayLine;

        public Transform leftScreenPointHand;
        public float ScreecPointPosOffset=21.02f;
        public Transform rightScreenPointHand;
        public static NoloHandleRay singleton { get; private set; }
        [SerializeField]
        private Camera mainCamera=null;
        public Camera MainCamera
        {
            get
            {
                return mainCamera;
            }
        }

        private void Awake()
        {
            singleton = this;
        }
        private void Update()
        {
            OnLeftHandleRay();
            OnLeftHandle();
            OnRightHandleRay();
            OnRightButton();
            if (_handRay == handRay.hand2d)
            {
                HandRay(rightHand, rightHandle);//todo
                HandRay_L(leftHandle);
            }
           

        }
        #region .射线交互
        [SerializeField]
        public Transform leftHandle;
        [SerializeField]
        public Transform rightHandle;
        [SerializeField]
        private GameObject leftLaserRay=null;
        [SerializeField]
        private GameObject rightLaserRay=null;//laser 激光
        [SerializeField]
        private LayerMask layerMask=0;
        [HideInInspector]
        public GameObject leftHandleObj = null;
        [HideInInspector]
        public GameObject rightHandleObj = null;


        private bool isHandleLeftObj;
        public bool IsHandleLeftObj
        {
            set
            {
                isHandleLeftObj = value;
            }
        }
        private Transform currentLeftTra;
        private Transform previousContact_L;         //射线进入推出
        private Ray _ray_L;
        public Ray Ray_L
        {
            get
            {
                return _ray_L;
            }
        }

        private Ray l_ray;//hoding时候的左手ray
        private Ray r_ray;//hoding时候的右手ray

        public void OnRefresh()
        {
            leftHandleObj = null;
            IsHandleLeftObj = false;
            rightHandleObj = null;
            IsHandleRightObj = false;
            Vector3 lineScal;
            lineScal.x = leftLaserRay.transform.localScale.x;
            lineScal.z = leftLaserRay.transform.localScale.z;
            lineScal.y = 500;
            leftLaserRay.transform.localScale = lineScal;
            rightLaserRay.transform.localScale = lineScal;
        }
        //左手
        private void OnLeftHandleRay()
        {
            Vector3 lineScal;
            lineScal.x = leftLaserRay.transform.localScale.x;
            lineScal.z = leftLaserRay.transform.localScale.z;

            RaycastHit hit;
            #region 使用2D小手时启用
            if (isHolding_L)//tqx
                _ray_L = new Ray(leftHandle.position, leftHandle.forward);
            else//tqx
                _ray_L = Camera.main.ScreenPointToRay(leftHand.position);
            #endregion

            #region 使用3D小手时启用
            //_ray_L = new Ray(leftHandle.position, leftHandle.forward);
            #endregion
            bool bHit = Physics.Raycast(_ray_L, out hit, 1000, layerMask);
            if (previousContact_L && previousContact_L != hit.transform)
            {
                if (!isHandleLeftObj && previousContact_L != null)
                {
                    OnRayExit(previousContact_L);
                    previousContact_L = null;
                }
            }
            if(bHit && previousContact_L != hit.transform)
            {
                if (leftHandleObj == null)
                {
                    OnRayEnter(hit.transform);
                    previousContact_L = hit.transform;
                    
                }

            }
            if (bHit)
            {
                //tqx_ 存储手柄发出的射线的物体
                hodingTempGo_L = hit.collider.gameObject;

                lineScal.y = hit.distance;
               
                leftLaserRay.transform.localScale = lineScal;

                if (hit.transform.gameObject == rightHandleObj)
                    return;
                //单次按下
                if (NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.Trigger))
                {
                    #region //tqx 改变小手状态  2D启用
                   ChangeHandIcon(leftHand, closehandpath);//tqx
                    #endregion
                    isHandleLeftObj = true;
                    currentLeftTra = hit.transform;
                    leftHandleObj = hit.transform.gameObject;
                    OnPickUp(NoloDeviceType.LeftController, hit.transform);
                }
            }
            else
            {
               
                lineScal.y = 500;
                if (!isHandleLeftObj)
                    leftLaserRay.transform.localScale = lineScal;
            }
            //手中持有物体
            if (isHandleLeftObj)
            {
                ////扳机持续按下
                //if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
                //{
                //    if(_handRay==handRay.hand2d)
                //    isHolding_L = true;//tqx
                //    //todo在按下的时候更新小手的位置即是跟随物体
                //    IsOnShootOfHandState(leftHand, leftHandleObj.transform, true);
                //    if (currentLeftTra == null)
                //    {
                //        isHandleLeftObj = false;
                //        return;
                //    }
                  
                //    //如果抓着的物体没隐藏，才抓着
                //    if (currentLeftTra.gameObject.activeInHierarchy == true)
                //    {
                //      //  Holding(currentLeftTra, _ray_L);
                //        Holding(currentLeftTra, _ray_L);//tqx
                //    }
                //    //抓着的物体隐藏了，那就放开它
                //    else
                //    {
                //        OnExit(NoloDeviceType.LeftController, currentLeftTra);
                //        leftHandleObj = null;
                //        isHandleLeftObj = false;
                //        currentLeftTra = null;
                //    }
                //    //新增判断
                //    if (currentLeftTra != null)
                //    {
                //        if (OnGetScript(currentLeftTra) != null)
                //        {
                //            //NoloVRInput.singleton.OnNoloLeftHapticPulse(100);
                //        }
                //    }
                //    #region//关闭这里射线不闪
                //    //if (currentLeftTra.gameObject != leftHandleObj)
                //    //    return;
                //    //else
                //    #endregion
                //    //MinLaser(hit.distance, "L");
                //    if(leftHandleObj)
                //    MinLaser(Vector3.Distance(leftHandle.position, leftHandleObj.transform.position), "L");
                //}
                //else if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
                //{
                //    if(_handRay==handRay.hand2d)
                //   isHolding_L = false;//tqx  

                //    if (currentLeftTra.gameObject != leftHandleObj)
                //        return;

                //    OnExit(NoloDeviceType.LeftController, currentLeftTra);
                //    leftHandleObj = null;
                //    isHandleLeftObj = false;
                //    currentLeftTra = null;
                //}
            }
            else
            {
                if (_handRay == handRay.hand2d)
                    isHolding_L = false;//tqx 
                hodingTempGo_L = null;
            }
        }
        void OnLeftHandle()
        {
            //扳机持续按下
            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
            {
                if (!isHandleLeftObj)
                    return;
                if (_handRay == handRay.hand2d)
                    isHolding_L = true;//tqx
                                       //todo在按下的时候更新小手的位置即是跟随物体
                IsOnShootOfHandState(leftHand, leftHandleObj.transform, true);
                if (currentLeftTra == null)
                {
                    isHandleLeftObj = false;
                    return;
                }

                //如果抓着的物体没隐藏，才抓着
                if (currentLeftTra.gameObject.activeInHierarchy == true)
                {
                    //  Holding(currentLeftTra, _ray_L);
                    Holding(currentLeftTra, _ray_L);//tqx
                }
                //抓着的物体隐藏了，那就放开它
                else
                {
                    OnExit(NoloDeviceType.LeftController, currentLeftTra);
                    leftHandleObj = null;
                    isHandleLeftObj = false;
                    currentLeftTra = null;
                }
                //新增判断
                if (currentLeftTra != null)
                {
                    if (OnGetScript(currentLeftTra) != null)
                    {
                        //NoloVRInput.singleton.OnNoloLeftHapticPulse(100);
                    }
                }
                #region//关闭这里射线不闪
                //if (currentLeftTra.gameObject != leftHandleObj)
                //    return;
                //else
                #endregion
                //MinLaser(hit.distance, "L");
                if (leftHandleObj)
                    MinLaser(Vector3.Distance(leftHandle.position, leftHandleObj.transform.position), "L");
            }
            else if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
            {
                if (!isHandleLeftObj)
                    return;
                if (_handRay == handRay.hand2d)
                    isHolding_L = false;//tqx  

                if (currentLeftTra.gameObject != leftHandleObj)
                    return;

                OnExit(NoloDeviceType.LeftController, currentLeftTra);
                leftHandleObj = null;
                isHandleLeftObj = false;
                currentLeftTra = null;
            }
        }
        private bool isHandleRightObj;//右手是否持有obj
        public bool IsHandleRightObj
        {
            set
            {
                isHandleRightObj = value;
            }
        }
        private Transform currentRightTra;
        private Transform previousContact_R;     //射线进入推出
        private Ray _ray_R;
        public Ray Ray_R
        {
            get
            {
                return _ray_R;
            }
        }
        
        #region//小手的功能及定义
        //public Transform handPoint;
        public Transform leftHand;
        public Transform rightHand;
        private string openhandpath = "openhand";
        private string closehandpath = "closehand";
        private GameObject tempGo_R;
        private GameObject hodingTempGo_R;//持续按下扳机键用来存储右手射线击中的物体
        private GameObject hodingTempGo_L;//持续按下扳机键用来存储左手手射线击中的物体
        private bool isHolding_R;//右手扳机键是否持续按下，在按下时候true,松开false，手中没有物体false
        private bool isHolding_L;//左手扳机键是否持续按下，在按下时候true,松开false，手中没有物体false
        private void Start()
        {
            if (_handRay == handRay.rayLine)
            {
                isHolding_L = true;
                isHolding_R = true;
            }
              
        }
        /// <summary>
        /// 改变小手的状态
        /// </summary>
        /// <param name="handicon"></param>
        public void ChangeHandIcon(Transform handPoint,string handicon)
        {
            #region/2d小手开启
            if (handPoint.GetComponent<Image>().sprite.name == handicon)
                return;
            handPoint.GetComponent<Image>().sprite = Resources.Load<Sprite>("handicon/" + handicon);
            #endregion
            #region//3D大手开启
            //if (handicon == "openhand")
            //    handPoint.GetChild(0).GetComponent<Animation>().Play("放");
            //if (handicon == "closehand")
            //    handPoint.GetChild(0).GetComponent<Animation>().Play("抓");
            #endregion
        }
        /// <summary>
        /// 改变小手图标在手柄的状态模式下
        /// </summary>
        private void ChangeHandIconOnNoLOState(Transform handPoint)
        {
            if (handPoint == rightHand)
            {
                #region//2ds手启用
                if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
                    ChangeHandIcon(handPoint, closehandpath);
                if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
                    ChangeHandIcon(handPoint, openhandpath);
                #endregion

                #region//3D手启用
                //if (NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.Trigger))
                //    ChangeHandIcon(handPoint, closehandpath);
                //if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
                //    ChangeHandIcon(handPoint, openhandpath);
                #endregion
            }
            else if(handPoint == leftHand)
            {
                #region//2ds手启用
                if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
                    ChangeHandIcon(handPoint, closehandpath);
                if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
                    ChangeHandIcon(handPoint, openhandpath);
                #endregion

                #region//3D手启用
                //if (NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.Trigger))
                //    ChangeHandIcon(handPoint, closehandpath);
                //if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
                //    ChangeHandIcon(handPoint, openhandpath);
                #endregion
            }

        }
        /// <summary>
        /// 击中物体时候小手的位置
        /// </summary>
        /// /// <param name="handPoint">需要传递左手或者右手</param>
        /// <param name="v3_">hit.point击中的位置</param>
        /// <param name="value_">是否击中</param>
        private void IsOnShootOfHandState(Transform handPoint, Transform handleObjTrans,bool value_)
        {
            #region//2D小手开启
            if (value_)
            {
                Vector3 var = Camera.main.WorldToScreenPoint(handleObjTrans.position);
                handPoint.position = new Vector3(var.x,var.y,0);
                if (handPoint == rightHand)
                    rightScreenPointHand.position = new Vector2(handPoint.position.x, handPoint.position.y- ScreecPointPosOffset);
                else if (handPoint == leftHand)
                    leftScreenPointHand.position = new Vector2(handPoint.position.x, handPoint.position.y- ScreecPointPosOffset);
            }
                // 击中物体的时候小手的位置更新
               
            else
            //没有击中物体的时候小手的位置更新
            {
                if (handPoint == rightHand)
                {
                    Vector3 var = Camera.main.WorldToScreenPoint(_ray_R.origin + _ray_R.direction * 500);
                    handPoint.position = new Vector3(var.x, var.y, 0);
                }
                else if (handPoint == leftHand)
                {
                    Vector3 var = Camera.main.WorldToScreenPoint(_ray_L.origin + _ray_L.direction * 500);
                    handPoint.position = new Vector3(var.x, var.y, 0);

                }
            }
            #endregion
            #region//3D大手

            //if (value_)
            //{
            //    //Debug.Log("持有的物体+"+ isHandleRightObj+"  抓住的位置=="+ handleObjTrans);
            //    if (isHandleRightObj)
            //        handPoint.position = handleObjTrans.position;//rightHandleObj.transform.position;
            //    if (isHandleLeftObj)
            //        handPoint.position = handleObjTrans.position;//leftHandleObj.transform.position;
            //}
            #endregion
        }
       //private void OnShoot(Transform handPoint,Transform handleObjTrans)
       // {
       //     handPoint.position = handleObjTrans.position;
       // }
        /// <summary>
        ///未击中物体跟手柄射线的右手
        /// </summary>
        private void HandRay(Transform handicon,Transform handDives)
        {
            #region//2D小手启用
            Ray HandRay = new Ray(handDives.position, handDives.forward);

            if (handDives == rightHandle)
                r_ray = HandRay;
            else if (handDives == leftHandle)
                l_ray = HandRay;
            if (!isHolding_R)
            {
                handicon.position = Camera.main.WorldToScreenPoint(HandRay.origin + HandRay.direction * 500);
                LimitHandPos(rightHand);
                ChangeHandIconOnNoLOState(handicon);
                rightScreenPointHand.position = new Vector2(handicon.position.x, handicon.position.y- ScreecPointPosOffset);
            }
            #endregion

            #region//3D大手启用
            //if (!isHolding_R)
            //{
            //    handicon.position = _ray_R.origin + _ray_R.direction * 2;
            //    ChangeHandIconOnNoLOState(handicon);//改变手的状态（抓取，张开）
            //}

            // LimitHandPos(rightHand);//限制手的位置

            #endregion
        }
        /// <summary>
        ////未击中物体跟手柄射线的左手
        /// </summary>
        private void HandRay_L(Transform handDives)
        {
            #region//2D小手 （使用2d小手启用）
            Ray HandRay = new Ray(handDives.position, handDives.forward);

            if (handDives == rightHandle)
                r_ray = HandRay;
            else if (handDives == leftHandle)
                l_ray = HandRay;
            if (!isHolding_L)
            {
                leftHand.position = Camera.main.WorldToScreenPoint(HandRay.origin + HandRay.direction * 500);
               
                LimitHandPos(leftHand);
                ChangeHandIconOnNoLOState(leftHand);
                leftScreenPointHand.position = new Vector2(leftHand.position.x, leftHand.position.y- ScreecPointPosOffset);//TODOpos
            }
            #endregion

            #region//3D大手
            //if (!isHolding_L)
            //{
            //    leftHand.position = _ray_L.origin + _ray_L.direction * 2;
            //    ChangeHandIconOnNoLOState(leftHand);//改变手的状态（抓取，张开）
            //}
            // LimitHandPos(rightHand);//限制手的位置

            #endregion

        }
        /// <summary>
        /// 限制小白手的屏幕范围
        /// </summary>
        /// <param name="handicon"></param>
        private void LimitHandPos(Transform handicon)
        {
            if (handicon.localPosition.y > 510)
            {
                handicon.localPosition = new Vector3(handicon.localPosition.x, 510, 0);
                
            }
            else if (handicon.localPosition.y < -516)
            {
                handicon.localPosition = new Vector3(handicon.localPosition.x, -516, 0);
               
            }
            if (handicon.localPosition.x < -941)
            {
                handicon.localPosition = new Vector3(-941, handicon.localPosition.y, 0);
            }
            else if (handicon.localPosition.x > 933)
            {
                handicon.localPosition = new Vector3(933, handicon.localPosition.y, 0);
            }
                
        }
       
        #endregion
        private void OnRightHandleRay()
        {
            RaycastHit hit;
            if (isHolding_R)//tqx_持续拿住的时候使用手柄射线,否则使用小手射线
                _ray_R = new Ray(rightHandle.position, rightHandle.forward);
            else
                _ray_R = Camera.main.ScreenPointToRay(rightHand.position);//从小白手发出射线
            bool bHit = Physics.Raycast(_ray_R, out hit, 1000, layerMask);
            if (previousContact_R && previousContact_R != hit.transform)
            {
                if (!isHandleRightObj && previousContact_R != null)
                {
                    OnRayExit(previousContact_R);
                    previousContact_R = null;
                }
            }
            if (bHit && previousContact_R != hit.transform)
            {
                if (rightHandleObj == null)
                {
                    OnRayEnter(hit.transform);
                    previousContact_R = hit.transform;
                }
            }
            if (bHit)
            {
                hodingTempGo_R = hit.collider.gameObject;
                rightLaserRay.transform.localScale = OnSetRayLine(hit.distance);
                if (hit.transform.gameObject == leftHandleObj)
                    return;
                if (NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.Trigger))
                {
                    ChangeHandIcon(rightHand, closehandpath);
                    isHandleRightObj = true;
                    OnPickUp(NoloDeviceType.RightController, hit.transform);
                    rightHandleObj = hit.transform.gameObject;
                    currentRightTra = hit.transform;
                }
                
            }
            else
            {
                if (!isHandleRightObj)
                    rightLaserRay.transform.localScale = OnSetRayLine(500);
            }
            if (isHandleRightObj)
            {
                //if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
                //{
                //    if(_handRay==handRay.hand2d)
                //        isHolding_R = true;//tqx                  
                //    IsOnShootOfHandState(rightHand, rightHandleObj.transform, true);
                //    if (currentRightTra == null)
                //    {
                //        isHandleRightObj = false;
                //        return;
                //    }
                //    if (currentRightTra.gameObject.activeInHierarchy == true) 
                //    {
                //        Holding(currentRightTra, _ray_R);
                //    }
                //    else 
                //    {
                //        OnExit(NoloDeviceType.RightController, currentRightTra);
                //        rightHandleObj = null;
                //        isHandleRightObj = false;
                //        currentRightTra = null;
                //    }
                //    if(rightHandleObj)
                //        MinLaser(Vector3.Distance(rightHandle.position, rightHandleObj.transform.position), "R");
                    

                //}
                //if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
                //{
                //    if (_handRay == handRay.hand2d)
                //        isHolding_R = false;//tqx 
                //    if (currentRightTra.gameObject != rightHandleObj)
                //        return;
                    
                //    OnExit(NoloDeviceType.RightController, currentRightTra);
                //    rightHandleObj = null;
                //    isHandleRightObj = false;
                //    currentRightTra = null;
                //}
            }
            else
            {
                if (_handRay == handRay.hand2d)
                    isHolding_R = false;//tqx
                hodingTempGo_R = null;
            }
        }
        void OnRightButton()
        {
            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
            {
                if (!isHandleRightObj)
                    return;
                if (_handRay == handRay.hand2d)
                    isHolding_R = true;//tqx                  
                IsOnShootOfHandState(rightHand, rightHandleObj.transform, true);
                if (currentRightTra == null)
                {
                    isHandleRightObj = false;
                    return;
                }
                if (currentRightTra.gameObject.activeInHierarchy == true)
                {
                    Holding(currentRightTra, _ray_R);
                }
                else
                {
                    OnExit(NoloDeviceType.RightController, currentRightTra);
                    rightHandleObj = null;
                    isHandleRightObj = false;
                    currentRightTra = null;
                }
                if (rightHandleObj)
                    MinLaser(Vector3.Distance(rightHandle.position, rightHandleObj.transform.position), "R");


            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
            {
                if (!isHandleRightObj)
                    return;
                if (_handRay == handRay.hand2d)
                    isHolding_R = false;//tqx 
                if (currentRightTra.gameObject != rightHandleObj)
                    return;

                OnExit(NoloDeviceType.RightController, currentRightTra);
                rightHandleObj = null;
                isHandleRightObj = false;
                currentRightTra = null;
            }
        }
        #endregion
        /// <summary>
        /// 设置最短激光，手中持有物体，在持续按下扳机时候调用
        /// </summary>
        private void MinLaser(float _dis,string leftOrRight)
        {
            if (leftOrRight == "L")
            {
                if (_dis <=0.02f)
                    leftLaserRay.transform.localScale = OnSetRayLine(0);
            }
            else if (leftOrRight == "R")
            {
                if (_dis <= 0.02f)
                {
                    rightLaserRay.transform.localScale = OnSetRayLine(0);
                }
            }
        }
        Vector3 OnSetRayLine(float value)
        {
            Vector3 LineScale;
            LineScale.x = leftLaserRay.transform.localScale.x;
            LineScale.z = leftLaserRay.transform.localScale.z;
            LineScale.y = value;
            return LineScale;
        }
        #region .拾取物体
        public void OnPickUp(NoloDeviceType var, Transform other)
        {
            OnGetScript(other)?.OnHandleTrigger(mainCamera.transform, var);
        }

        public void Holding(Transform other,Ray var)
        {
            OnGetScript(other)?.OnHoldingTrigger(var);
        }

        private void OnExit(NoloDeviceType var,Transform other)
        {
            OnGetScript(other)?.OnReleseTrigger(var);
        }

        private void OnRayEnter(Transform other)
        {
            OnGetScript(other)?.OnRayEnter();
        }

        private void OnRayExit(Transform other)
        {
            OnGetScript(other)?.OnRayExit();
        }

        private GraspingObjectBase OnGetScript(Transform other)
        {
            GraspingObjectBase script = other.GetComponentInParent<GraspingObjectBase>();
            if (script == null)
                return null;
            if (script.enabled == false)
                return null;
            return script;
        }
        #endregion
    }


}

