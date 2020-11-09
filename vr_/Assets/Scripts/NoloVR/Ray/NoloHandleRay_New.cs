using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NoloVR
{
   
    public class NoloHandleRay_New : MonoBehaviour
    {
        #region .新版
        [Header("默认相机")]
        [SerializeField]
        private Camera mainCamera = null;
        public Camera MainCamera
        {
            get
            {
                return mainCamera;
            }
        }
        [Header("射线信息")]
        [Space(10)]
        [Tooltip("可供射线交互的层")]
        [SerializeField]
        LayerMask layerMask = 0;
        [Header("左手")]
        [Tooltip("左手发出射线位置")]
        [SerializeField]
        Transform leftSendRayTra;
        public Transform leftHandle
        {
            get
            {
                return leftSendRayTra;
            }
        }
        [Tooltip("左手射线")]
        [SerializeField]
        Transform leftLaserRay;
        public Ray Ray_L
        {
            get;private set;
        }
        [HideInInspector]
        public GameObject leftHandleObj = null;   //左手正在拾取的物体
        public bool isPressLeft { get; set; } //是否处于拾取状态
        [Header("右手")]
        [Tooltip("右手发出射线位置")]
        [SerializeField]
        Transform rightSendRayTra;
        public Transform rightHandle
        {
            get
            {
                return rightSendRayTra;
            }
        }
        [Tooltip("右手射线")]
        [SerializeField]
        Transform rightLaserRay;
        public Ray Ray_R
        {
            get;private set;
        }
        [HideInInspector]
        public GameObject rightHandleObj = null;   //右手正在拾取的物体
        public bool isPressRight { get; set; } //是否处于拾取状态
        static NoloHandleRay_New _singleton;
        public static NoloHandleRay_New singleton
        {
            get
            {
                return _singleton;
            }
        }
        private void Awake()
        {
            _singleton = this;
            isPressLeft = false;
            isPressRight = false;
        }
        private void Update()
        {
            OnLeftHandleRay();
            OnLeftButton();
            OnRightHandleRay();
            OnRightButton();
        }
        //左手发射射线交互
        void OnLeftHandleRay()
        {
            Ray handleRay = new Ray(leftSendRayTra.position, leftSendRayTra.forward);
            Ray_L = handleRay;
            RaycastHit hit;
            bool bHit = Physics.Raycast(handleRay, out hit, 1000, layerMask);
            if (rightHandleObj != null && rightHandleObj == hit.transform)
                return;
            if (leftHandleObj != null && leftHandleObj.transform != hit.transform)
            {
                if (!isPressLeft)
                {
                    Debug.Log("左手射线退出物体:" + leftHandleObj.transform);
                    OnRayExit(leftHandleObj.transform);
                    leftHandleObj = null;
                }
            }
            if (bHit)
            {
                if (!isPressLeft)
                {
                    if (leftHandleObj == null)
                    {
                        Debug.Log("左手物体为空，左手射线进入物体:" + hit.transform.name);
                        leftHandleObj = hit.transform.gameObject;
                        OnRayEnter(leftHandleObj.transform);
                    }
                    else
                    {
                        if (leftHandleObj.transform != hit.transform)
                        {
                            Debug.Log("左手物体不等于射线指向物体，左手射线进入物体:" + hit.transform.name);
                            leftHandleObj = hit.transform.gameObject;
                            OnRayEnter(leftHandleObj.transform);
                        }
                    }
                }
            }
            if (leftHandleObj == null)
                leftLaserRay.localScale = OnSetRayLine(1000);
            else
            {
                leftLaserRay.localScale = OnSetRayLine(hit.distance);
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.Trigger))
            {
                if (leftHandleObj == null)
                    return;
                else
                {
                    isPressLeft = true;
                    OnPickUp(NoloDeviceType.LeftController, leftHandleObj.transform);
                }
            }

        }
        void OnLeftButton()
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
            {
                if (leftHandleObj == null)
                    return;
                else
                {
                    isPressLeft = true;
                    Holding(leftHandleObj.transform, Ray_L);
                }
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
            {
                if (leftHandleObj == null)
                    return;
                else
                {
                    isPressLeft = false;
                    OnExit(NoloDeviceType.BaseStation, leftHandleObj.transform);
                }
            }
        }
        //右手发射射线交互
        void OnRightHandleRay()
        {
            Ray handleRay = new Ray(rightSendRayTra.position, rightSendRayTra.forward);
            Ray_R = handleRay;
            RaycastHit hit;
            bool bHit = Physics.Raycast(handleRay, out hit, 1000, layerMask);
            if (leftHandleObj != null && leftHandleObj.transform == hit.transform)
                return;
            if (rightHandleObj != null && rightHandleObj.transform != hit.transform)
            {
                if (!isPressRight)
                {
                    Debug.Log("右手射线退出物体:" + rightHandleObj.transform);
                    OnRayExit(rightHandleObj.transform);
                    rightHandleObj = null;
                }
            }
            if (bHit)
            {
                if (!isPressRight)
                {
                    if (rightHandleObj == null)
                    {
                        Debug.Log("右手物体为空，右手射线进入物体:" + hit.transform.name);
                        rightHandleObj = hit.transform.gameObject;
                        OnRayEnter(rightHandleObj.transform);
                    }
                    else
                    {
                        if (rightHandleObj.transform != hit.transform)
                        {
                            Debug.Log("右手物体不等于射线指向物体，右手射线进入物体:" + hit.transform.name);
                            rightHandleObj = hit.transform.gameObject;
                            OnRayEnter(rightHandleObj.transform);
                        }
                    }
                }
            }
            if (rightHandleObj == null)
                rightLaserRay.localScale = OnSetRayLine(1000);
            else
            {
                rightLaserRay.localScale = OnSetRayLine(hit.distance);
            }
            if (NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.Trigger))
            {
                if (rightHandleObj == null)
                    return;
                else
                {
                    isPressRight = true;
                    OnPickUp(NoloDeviceType.RightController, rightHandleObj.transform);
                }
            }
        }
        void OnRightButton()
        {
            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
            {
                if (rightHandleObj == null)
                    return;
                else
                {
                    isPressRight = true;
                    Holding(rightHandleObj.transform, Ray_R);
                }
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
            {
                if (rightHandleObj == null)
                    return;
                else
                {
                    isPressRight = false;
                    OnExit(NoloDeviceType.BaseStation, rightHandleObj.transform);
                }
            }
        }
        public void OnRefresh()
        {
            isPressLeft = false;
            leftHandleObj = null;
            leftLaserRay.localScale = OnSetRayLine(1000);
            isPressRight = false;
            rightHandleObj = null;
            rightLaserRay.localScale = OnSetRayLine(1000);
        }

        Vector3 OnSetRayLine(float value)
        {
            Vector3 LineScale;
            LineScale.x = leftLaserRay.localScale.x;
            LineScale.z = leftLaserRay.localScale.z;
            LineScale.y = value;
            return LineScale;
        }
        //拾取物体
        public void OnPickUp(NoloDeviceType var, Transform other)
        {
            OnGetScript(other)?.OnHandleTrigger(mainCamera.transform, var);
        }
        public void Holding(Transform other, Ray var)
        {
            OnGetScript(other)?.OnHoldingTrigger(var);
        }
        private void OnExit(NoloDeviceType var, Transform other)
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
        #region .旧版
        // public enum handRay
        // {
        //     rayLine,
        //     hand2d,
        //     hand3d
        // }
        // public handRay _handRay = handRay.rayLine;

        // public Transform leftScreenPointHand;
        // public float ScreecPointPosOffset=21.02f;
        // public Transform rightScreenPointHand;
        // public static NoloHandleRay singleton { get; private set; }


        // private void Awake()
        // {
        //     singleton = this;
        // }
        // private void Update()
        // {
        //     OnLeftHandleRay();
        //     OnRightHandleRay();
        //     if (_handRay == handRay.hand2d)
        //     {
        //         HandRay(rightHand, rightHandle);//todo
        //         //HandRay(leftHand, leftHandle);//todo
        //         HandRay_L(leftHandle);
        //     }


        // }
        // #region .射线交互
        // [SerializeField]
        // public Transform leftHandle;
        // [SerializeField]
        // public Transform rightHandle;
        // [SerializeField]
        // private GameObject leftLaserRay=null;
        // [SerializeField]
        // private GameObject rightLaserRay=null;//laser 激光
        // [SerializeField]
        // private LayerMask layerMask=0;
        // [HideInInspector]
        // public GameObject leftHandleObj = null;
        // [HideInInspector]
        // public GameObject rightHandleObj = null;


        // private bool isHandleLeftObj;
        // public bool IsHandleLeftObj
        // {
        //     set
        //     {
        //         isHandleLeftObj = value;
        //     }
        // }
        // private Transform currentLeftTra;
        // private Transform previousContact_L;         //射线进入推出
        // private Ray _ray_L;
        // public Ray Ray_L
        // {
        //     get
        //     {
        //         return _ray_L;
        //     }
        // }

        // private Ray l_ray;//hoding时候的左手ray
        // private Ray r_ray;//hoding时候的右手ray

        // public void OnRefresh()
        // {
        //     leftHandleObj = null;
        //     IsHandleLeftObj = false;
        //     rightHandleObj = null;
        //     IsHandleRightObj = false;
        //     Vector3 lineScal;
        //     lineScal.x = leftLaserRay.transform.localScale.x;
        //     lineScal.z = leftLaserRay.transform.localScale.z;
        //     lineScal.y = 500;
        //     leftLaserRay.transform.localScale = lineScal;
        //     rightLaserRay.transform.localScale = lineScal;
        // }
        // //左手
        // private void OnLeftHandleRay()
        // {
        //     Vector3 lineScal;
        //     lineScal.x = leftLaserRay.transform.localScale.x;
        //     lineScal.z = leftLaserRay.transform.localScale.z;

        //     RaycastHit hit;
        //     #region 使用2D小手时启用
        //     if (isHolding_L)//tqx
        //         _ray_L = new Ray(leftHandle.position, leftHandle.forward);
        //     else//tqx
        //         _ray_L = Camera.main.ScreenPointToRay(leftHand.position);
        //     #endregion

        //     #region 使用3D小手时启用
        //     //_ray_L = new Ray(leftHandle.position, leftHandle.forward);
        //     #endregion
        //     bool bHit = Physics.Raycast(_ray_L, out hit, 1000, layerMask);
        //     if (previousContact_L && previousContact_L != hit.transform)
        //     {
        //         if (!isHandleLeftObj && previousContact_L != null)
        //         {
        //             OnRayExit(previousContact_L);
        //             previousContact_L = null;
        //         }
        //     }
        //     if(bHit && previousContact_L != hit.transform)
        //     {
        //         if (leftHandleObj == null)
        //         {
        //             OnRayEnter(hit.transform);
        //             previousContact_L = hit.transform;

        //         }

        //     }
        //     if (bHit)
        //     {
        //         //tqx_ 存储手柄发出的射线的物体
        //         hodingTempGo_L = hit.collider.gameObject;

        //         lineScal.y = hit.distance;

        //         leftLaserRay.transform.localScale = lineScal;

        //         if (hit.transform.gameObject == rightHandleObj)
        //             return;
        //         //单次按下
        //         if (NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.Trigger))
        //         {
        //             #region //tqx 改变小手状态  2D启用
        //            ChangeHandIcon(leftHand, closehandpath);//tqx
        //             #endregion
        //             isHandleLeftObj = true;
        //             currentLeftTra = hit.transform;
        //             leftHandleObj = hit.transform.gameObject;
        //             OnPickUp(NoloDeviceType.LeftController, hit.transform);
        //         }
        //     }
        //     else
        //     {

        //         lineScal.y = 500;
        //         if (!isHandleLeftObj)
        //             leftLaserRay.transform.localScale = lineScal;
        //     }
        //     //手中持有物体
        //     if (isHandleLeftObj)
        //     {
        //         //扳机持续按下
        //         if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
        //         {
        //             if(_handRay==handRay.hand2d)
        //             isHolding_L = true;//tqx
        //             //todo在按下的时候更新小手的位置即是跟随物体
        //             IsOnShootOfHandState(leftHand, leftHandleObj.transform, true);
        //             if (currentLeftTra == null)
        //             {
        //                 isHandleLeftObj = false;
        //                 return;
        //             }

        //             //如果抓着的物体没隐藏，才抓着
        //             if (currentLeftTra.gameObject.activeInHierarchy == true)
        //             {
        //               //  Holding(currentLeftTra, _ray_L);
        //                 Holding(currentLeftTra, _ray_L);//tqx
        //             }
        //             //抓着的物体隐藏了，那就放开它
        //             else
        //             {
        //                 OnExit(NoloDeviceType.LeftController, currentLeftTra);
        //                 leftHandleObj = null;
        //                 isHandleLeftObj = false;
        //                 currentLeftTra = null;
        //             }
        //             //新增判断
        //             if (currentLeftTra != null)
        //             {
        //                 if (OnGetScript(currentLeftTra) != null)
        //                 {
        //                     //NoloVRInput.singleton.OnNoloLeftHapticPulse(100);
        //                 }
        //             }
        //             #region//关闭这里射线不闪
        //             //if (currentLeftTra.gameObject != leftHandleObj)
        //             //    return;
        //             //else
        //             #endregion
        //             //MinLaser(hit.distance, "L");
        //             if(leftHandleObj)
        //             MinLaser(Vector3.Distance(leftHandle.position, leftHandleObj.transform.position), "L");
        //         }
        //         else if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
        //         {
        //             if(_handRay==handRay.hand2d)
        //            isHolding_L = false;//tqx  

        //             if (currentLeftTra.gameObject != leftHandleObj)
        //                 return;

        //             OnExit(NoloDeviceType.LeftController, currentLeftTra);
        //             leftHandleObj = null;
        //             isHandleLeftObj = false;
        //             currentLeftTra = null;
        //         }
        //     }
        //     else
        //     {
        //         if (_handRay == handRay.hand2d)
        //             isHolding_L = false;//tqx 
        //         hodingTempGo_L = null;
        //     }
        // }

        // private bool isHandleRightObj;//右手是否持有obj
        // public bool IsHandleRightObj
        // {
        //     set
        //     {
        //         isHandleRightObj = value;
        //     }
        // }
        // private Transform currentRightTra;
        // private Transform previousContact_R;     //射线进入推出
        // private Ray _ray_R;
        // public Ray Ray_R
        // {
        //     get
        //     {
        //         return _ray_R;
        //     }
        // }

        // #region//小手的功能及定义
        // //public Transform handPoint;
        // public Transform leftHand;
        // public Transform rightHand;
        // private string openhandpath = "openhand";
        // private string closehandpath = "closehand";
        // private GameObject tempGo_R;
        // private GameObject hodingTempGo_R;//持续按下扳机键用来存储右手射线击中的物体
        // private GameObject hodingTempGo_L;//持续按下扳机键用来存储左手手射线击中的物体
        // private bool isHolding_R;//右手扳机键是否持续按下，在按下时候true,松开false，手中没有物体false
        // private bool isHolding_L;//左手扳机键是否持续按下，在按下时候true,松开false，手中没有物体false
        // private void Start()
        // {
        //     if (_handRay == handRay.rayLine)
        //     {
        //         isHolding_L = true;
        //         isHolding_R = true;
        //     }

        // }
        // /// <summary>
        // /// 改变小手的状态
        // /// </summary>
        // /// <param name="handicon"></param>
        // public void ChangeHandIcon(Transform handPoint,string handicon)
        // {
        //     #region/2d小手开启
        //     if (handPoint.GetComponent<Image>().sprite.name == handicon)
        //         return;
        //     handPoint.GetComponent<Image>().sprite = Resources.Load<Sprite>("handicon/" + handicon);
        //     #endregion
        //     #region//3D大手开启
        //     //if (handicon == "openhand")
        //     //    handPoint.GetChild(0).GetComponent<Animation>().Play("放");
        //     //if (handicon == "closehand")
        //     //    handPoint.GetChild(0).GetComponent<Animation>().Play("抓");
        //     #endregion
        // }
        // /// <summary>
        // /// 改变小手图标在手柄的状态模式下
        // /// </summary>
        // private void ChangeHandIconOnNoLOState(Transform handPoint)
        // {
        //     if (handPoint == rightHand)
        //     {
        //         #region//2ds手启用
        //         if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
        //             ChangeHandIcon(handPoint, closehandpath);
        //         if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
        //             ChangeHandIcon(handPoint, openhandpath);
        //         #endregion

        //         #region//3D手启用
        //         //if (NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.Trigger))
        //         //    ChangeHandIcon(handPoint, closehandpath);
        //         //if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
        //         //    ChangeHandIcon(handPoint, openhandpath);
        //         #endregion
        //     }
        //     else if(handPoint == leftHand)
        //     {
        //         #region//2ds手启用
        //         if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
        //             ChangeHandIcon(handPoint, closehandpath);
        //         if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
        //             ChangeHandIcon(handPoint, openhandpath);
        //         #endregion

        //         #region//3D手启用
        //         //if (NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.Trigger))
        //         //    ChangeHandIcon(handPoint, closehandpath);
        //         //if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
        //         //    ChangeHandIcon(handPoint, openhandpath);
        //         #endregion
        //     }

        // }
        // /// <summary>
        // /// 击中物体时候小手的位置
        // /// </summary>
        // /// /// <param name="handPoint">需要传递左手或者右手</param>
        // /// <param name="v3_">hit.point击中的位置</param>
        // /// <param name="value_">是否击中</param>
        // private void IsOnShootOfHandState(Transform handPoint, Transform handleObjTrans,bool value_)
        // {
        //     #region//2D小手开启
        //     if (value_)
        //     {
        //         Vector3 var = Camera.main.WorldToScreenPoint(handleObjTrans.position);
        //         handPoint.position = new Vector3(var.x,var.y,0);
        //         if (handPoint == rightHand)
        //             rightScreenPointHand.position = new Vector2(handPoint.position.x, handPoint.position.y- ScreecPointPosOffset);
        //         else if (handPoint == leftHand)
        //             leftScreenPointHand.position = new Vector2(handPoint.position.x, handPoint.position.y- ScreecPointPosOffset);
        //     }
        //         // 击中物体的时候小手的位置更新

        //     else
        //     //没有击中物体的时候小手的位置更新
        //     {
        //         if (handPoint == rightHand)
        //         {
        //             Vector3 var = Camera.main.WorldToScreenPoint(_ray_R.origin + _ray_R.direction * 500);
        //             handPoint.position = new Vector3(var.x, var.y, 0);
        //         }
        //         else if (handPoint == leftHand)
        //         {
        //             Vector3 var = Camera.main.WorldToScreenPoint(_ray_L.origin + _ray_L.direction * 500);
        //             handPoint.position = new Vector3(var.x, var.y, 0);

        //         }
        //     }
        //     #endregion
        //     #region//3D大手

        //     //if (value_)
        //     //{
        //     //    //Debug.Log("持有的物体+"+ isHandleRightObj+"  抓住的位置=="+ handleObjTrans);
        //     //    if (isHandleRightObj)
        //     //        handPoint.position = handleObjTrans.position;//rightHandleObj.transform.position;
        //     //    if (isHandleLeftObj)
        //     //        handPoint.position = handleObjTrans.position;//leftHandleObj.transform.position;
        //     //}
        //     #endregion
        // }
        ////private void OnShoot(Transform handPoint,Transform handleObjTrans)
        //// {
        ////     handPoint.position = handleObjTrans.position;
        //// }
        // /// <summary>
        // ///未击中物体跟手柄射线的右手
        // /// </summary>
        // private void HandRay(Transform handicon,Transform handDives)
        // {
        //     #region//2D小手启用
        //     Ray HandRay = new Ray(handDives.position, handDives.forward);

        //     if (handDives == rightHandle)
        //         r_ray = HandRay;
        //     else if (handDives == leftHandle)
        //         l_ray = HandRay;
        //     if (!isHolding_R)
        //     {
        //         handicon.position = Camera.main.WorldToScreenPoint(HandRay.origin + HandRay.direction * 500);
        //         LimitHandPos(rightHand);
        //         ChangeHandIconOnNoLOState(handicon);
        //         rightScreenPointHand.position = new Vector2(handicon.position.x, handicon.position.y- ScreecPointPosOffset);
        //     }
        //     #endregion

        //     #region//3D大手启用
        //     //if (!isHolding_R)
        //     //{
        //     //    handicon.position = _ray_R.origin + _ray_R.direction * 2;
        //     //    ChangeHandIconOnNoLOState(handicon);//改变手的状态（抓取，张开）
        //     //}

        //     // LimitHandPos(rightHand);//限制手的位置

        //     #endregion
        // }
        // /// <summary>
        // ////未击中物体跟手柄射线的左手
        // /// </summary>
        // private void HandRay_L(Transform handDives)
        // {
        //     #region//2D小手 （使用2d小手启用）
        //     Ray HandRay = new Ray(handDives.position, handDives.forward);

        //     if (handDives == rightHandle)
        //         r_ray = HandRay;
        //     else if (handDives == leftHandle)
        //         l_ray = HandRay;
        //     if (!isHolding_L)
        //     {
        //         leftHand.position = Camera.main.WorldToScreenPoint(HandRay.origin + HandRay.direction * 500);

        //         LimitHandPos(leftHand);
        //         ChangeHandIconOnNoLOState(leftHand);
        //         leftScreenPointHand.position = new Vector2(leftHand.position.x, leftHand.position.y- ScreecPointPosOffset);//TODOpos
        //     }
        //     #endregion

        //     #region//3D大手
        //     //if (!isHolding_L)
        //     //{
        //     //    leftHand.position = _ray_L.origin + _ray_L.direction * 2;
        //     //    ChangeHandIconOnNoLOState(leftHand);//改变手的状态（抓取，张开）
        //     //}
        //     // LimitHandPos(rightHand);//限制手的位置

        //     #endregion

        // }
        // /// <summary>
        // /// 限制小白手的屏幕范围
        // /// </summary>
        // /// <param name="handicon"></param>
        // private void LimitHandPos(Transform handicon)
        // {
        //     if (handicon.localPosition.y > 510)
        //     {
        //         handicon.localPosition = new Vector3(handicon.localPosition.x, 510, 0);

        //     }
        //     else if (handicon.localPosition.y < -516)
        //     {
        //         handicon.localPosition = new Vector3(handicon.localPosition.x, -516, 0);

        //     }
        //     if (handicon.localPosition.x < -941)
        //     {
        //         handicon.localPosition = new Vector3(-941, handicon.localPosition.y, 0);
        //     }
        //     else if (handicon.localPosition.x > 933)
        //     {
        //         handicon.localPosition = new Vector3(933, handicon.localPosition.y, 0);
        //     }

        // }

        // #endregion
        // private void OnRightHandleRay()
        // {
        //     Vector3 lineScal;
        //     lineScal.x = rightLaserRay.transform.localScale.x;
        //     lineScal.z = rightLaserRay.transform.localScale.z;
        //     RaycastHit hit;
        //     #region//使用2D小手启用
        //     if (isHolding_R)//tqx_持续拿住的时候使用手柄射线,否则使用小手射线
        //         _ray_R = new Ray(rightHandle.position, rightHandle.forward);
        //     else
        //         _ray_R = Camera.main.ScreenPointToRay(rightHand.position);//从小白手发出射线
        //     #endregion

        //     #region//使用3D小手启用
        //     //_ray_R = new Ray(rightHandle.position, rightHandle.forward);
        //     #endregion

        //     bool bHit = Physics.Raycast(_ray_R, out hit, 1000, layerMask);
        //     #region//射线退出
        //     //如果上一个接触者不为空并且上一个接触者不等于当前接触者
        //     //如果右手未持有物体，并且当前接触者不等空
        //     //射线退出上一个接触者
        //     //上一个接触者设置为空
        //     #endregion
        //     if (previousContact_R && previousContact_R != hit.transform)
        //     {
        //         if (!isHandleRightObj && previousContact_R != null)
        //         {
        //             OnRayExit(previousContact_R);
        //             previousContact_R = null;
        //         }
        //     }
        //     #region//射线进入
        //     //如果持有当前接触者并且上一个接触者不等于当前接触者
        //     //认为射线进入当前接触者
        //     //上一个接触者设置为当前接触者
        //     #endregion
        //     if (bHit && previousContact_R != hit.transform)
        //     {
        //         if (rightHandleObj == null)
        //         {
        //             OnRayEnter(hit.transform);
        //             previousContact_R = hit.transform;

        //         }
        //     }
        //     #region//击中物体，持有物体
        //     //击中物体后把射线长度设置为从手柄到物体的长度
        //     //当按下右手柄扳机后
        //     //捡起物体，设置右手物体
        //     #endregion
        //     if (bHit)
        //     {
        //         Debug.DrawLine(_ray_R.origin, hit.point, Color.red); 
        //         //////击中物体的时候小手的位置更新
        //         ////IsOnShootOfHandState(rightHand, hit.point, true);
        //         //tqx_ 存储手柄发出的射线的物体
        //         hodingTempGo_R = hit.collider.gameObject;

        //         //激光长度缩放设置为从手柄到物体的距离
        //         lineScal.y = hit.distance;
        //         //rightHand.position = Camera.main.WorldToScreenPoint(r_ray.origin + r_ray.direction * hit.distance);
        //         rightLaserRay.transform.localScale = lineScal;
        //         if (hit.transform.gameObject == leftHandleObj)
        //             return;
        //         //单次按下
        //         if (NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.Trigger))
        //         {
        //             #region //tqx 改变小手状态  2D启用
        //             ChangeHandIcon(rightHand, closehandpath);
        //             #endregion
        //             isHandleRightObj = true;
        //             OnPickUp(NoloDeviceType.RightController, hit.transform);
        //             rightHandleObj = hit.transform.gameObject;
        //             currentRightTra = hit.transform;
        //          //   Debug.Log(rightHandleObj+"1234");
        //         }

        //     }
        //     //没击中射线不变
        //     else
        //     {
        //         //////没有击中物体的时候小手的位置更新
        //         ////IsOnShootOfHandState(rightHand, hit.point, false);
        //         lineScal.y = 500;
        //         if (!isHandleRightObj)
        //             rightLaserRay.transform.localScale = lineScal;
        //         //if (currentRightTra.gameObject == rightHandleObj)
        //         //    rightLaserRay.transform.localScale = new Vector3(rightLaserRay.transform.localScale.x, 0, rightLaserRay.transform.localScale.z);
        //     }
        //     //手中持有物体
        //     if (isHandleRightObj)
        //     {

        //         //手柄持续按下按键
        //         if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
        //         {
        //             //Debug.Log("持有的物体++"+rightHandleObj);
        //             if(_handRay==handRay.hand2d)
        //             isHolding_R = true;//tqx
        //                                //todo//tqx                    
        //             IsOnShootOfHandState(rightHand, rightHandleObj.transform, true);

        //             if (currentRightTra == null)
        //             {
        //                 isHandleRightObj = false;
        //                 return;
        //             }



        //             //如果抓着的物体没隐藏，才抓着
        //             if (currentRightTra.gameObject.activeInHierarchy == true) 
        //             {
        //                 //Debug.LogError("物体没隐藏");
        //                 Holding(currentRightTra, _ray_R);
        //                 //Holding(currentRightTra, r_ray);//tqx
        //             }
        //             //抓着的物体隐藏了，那就放开它
        //             else 
        //             {
        //                // Debug.LogError("物体隐藏了，放开");
        //                 OnExit(NoloDeviceType.RightController, currentRightTra);
        //                 rightHandleObj = null;
        //                 isHandleRightObj = false;
        //                 currentRightTra = null;
        //             }
        //             //新增判断
        //             if (currentRightTra!=null)
        //             {
        //                 if (OnGetScript(currentRightTra) != null)
        //                 {
        //                     //NoloVRInput.singleton.OnNoloRightHapticPulse(100);
        //                 }
        //             }
        //             #region//关闭这里射线不闪
        //             //if (currentRightTra.gameObject != rightHandleObj)
        //             //    return;
        //             //else 
        //             #endregion
        //             if(rightHandleObj)
        //             MinLaser(Vector3.Distance(rightHandle.position, rightHandleObj.transform.position), "R");


        //         }
        //         else if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
        //         {
        //             if (_handRay == handRay.hand2d)
        //                 isHolding_R = false;//tqx 
        //             ////ChangeHandIcon(rightHand, openhandpath);
        //             if (currentRightTra.gameObject != rightHandleObj)
        //                 return;

        //             OnExit(NoloDeviceType.RightController, currentRightTra);
        //             rightHandleObj = null;
        //             isHandleRightObj = false;
        //             currentRightTra = null;
        //         }
        //     }
        //     else
        //     {
        //         if (_handRay == handRay.hand2d)
        //             isHolding_R = false;//tqx
        //         hodingTempGo_R = null;
        //         ////ChangeHandIconOnNoLOState(rightHand);

        //     }
        // }
        // #endregion
        // /// <summary>
        // /// 设置最短激光，手中持有物体，在持续按下扳机时候调用
        // /// </summary>
        // private void MinLaser(float _dis,string leftOrRight)
        // {
        //     //Debug.Log(_dis+"+++射线距离++"+leftHandleObj.name+"++左手的东西++"+rightHandleObj+"右手的东西");
        //     if (leftOrRight == "L")
        //     {
        //         if (_dis <=0.02f)
        //             leftLaserRay.transform.localScale = new Vector3(leftLaserRay.transform.localScale.x, 0, leftLaserRay.transform.localScale.z);
        //     }
        //     else if (leftOrRight == "R")
        //     {
        //         if (_dis <= 0.02f)
        //         {
        //             rightLaserRay.transform.localScale = new Vector3(rightLaserRay.transform.localScale.x, 0, rightLaserRay.transform.localScale.z);
        //         }
        //     }
        // }
        // //拾取物体
        // public void OnPickUp(NoloDeviceType var, Transform other)
        // {
        //     OnGetScript(other)?.OnHandleTrigger(mainCamera.transform, var);
        // }

        // public void Holding(Transform other,Ray var)
        // {
        //     OnGetScript(other)?.OnHoldingTrigger(var);
        // }

        // private void OnExit(NoloDeviceType var,Transform other)
        // {
        //     OnGetScript(other)?.OnReleseTrigger(var);
        // }

        // private void OnRayEnter(Transform other)
        // {
        //     OnGetScript(other)?.OnRayEnter();
        // }

        // private void OnRayExit(Transform other)
        // {
        //     OnGetScript(other)?.OnRayExit();
        // }

        // private GraspingObjectBase OnGetScript(Transform other)
        // {
        //     GraspingObjectBase script = other.GetComponentInParent<GraspingObjectBase>();
        //     if (script == null)
        //         return null;
        //     if (script.enabled == false)
        //         return null;
        //     return script;
        // }
        // private void OnGUI()
        // {
        //     //GL.LoadOrtho();
        //     //GL.Begin(GL.LINES);
        //     //GL.Color(Color.red);
        //     //Vector3 posion = Camera.main.WorldToScreenPoint(leftHand.position);
        //     //GL.Vertex3(posion.x, posion.y, posion.z);
        //     //GL.Vertex3(leftHand.position.x, leftHand.position.y, leftHand.position.z);
        //     //GL.End();
        // }
        #endregion
    }


}

