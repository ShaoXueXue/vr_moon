using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 手柄管理类
/// </summary>
public class NoloHandleManager : MonoBehaviour
{

    //单例模式
    public static NoloHandleManager Instance;

    // Start is called before the first frame update
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Awake()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        Instance = this;
    }
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Start()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        
    }

#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void OnDestroy()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
       
    }

    // Update is called once per frame
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Update()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        LeftHandle();
        RightHandle();
        LeftLaserRayRenderer();
        RightLaserRayRenderer();
    }

    #region 左手柄按键委托
    //左手柄委托
    public delegate void NoloLeftHandle();

    //左手柄触摸板按键按下
    public NoloLeftHandle leftTouchPadDown;
    //左手柄触摸板按键抬起
    public NoloLeftHandle leftTouchPadUp;
    //左手柄触摸板按键持续按住
    public NoloLeftHandle leftTouchPadDownKeep;

    //左手柄扳机按下
    public NoloLeftHandle leftTriggerDown;
    //左手柄扳机抬起
    public NoloLeftHandle leftTriggerUp;
    //左手柄扳机持续按住
    public NoloLeftHandle leftTriggerDownKeep;

    //左手柄系统按键（电源键）按下
    public NoloLeftHandle leftSystemDown;
    //左手柄系统按键（电源键）抬起
    public NoloLeftHandle leftSystemUp;
    //左手柄系统按键（电源键）持续按下
    public NoloLeftHandle leftSystemDownKeep;

    //左手柄返回按键按下
    public NoloLeftHandle leftBackDown;
    //左手柄返回按键抬起
    public NoloLeftHandle leftBackUp;
    //左手柄返回按键持续按下
    public NoloLeftHandle leftBackDownKeep;

    //左手柄音量减键按下
    public NoloLeftHandle leftVolumeDownDown;
    //左手柄音量减键抬起
    public NoloLeftHandle leftVolumeDownUp;
    //左手柄音量减键持续按下
    public NoloLeftHandle leftVolumeDownDownKeep;

    //左手柄音量加键按下
    public NoloLeftHandle leftVolumeUpDown;
    //左手柄音量加键抬起
    public NoloLeftHandle leftVolumeUpUp;
    //左手柄音量加键持续按下
    public NoloLeftHandle leftVolumeUpDownKeep;
    #endregion
    #region 右手柄按键委托
    //右手柄委托
    public delegate void NoloRightHandle();

    //左手柄触摸板按键按下
    public NoloRightHandle rightTouchPadDown;
    //左手柄触摸板按键抬起
    public NoloRightHandle rightTouchPadUp;
    //左手柄触摸板按键持续按住
    public NoloRightHandle rightTouchPadDownKeep;

    //左手柄扳机按下
    public NoloRightHandle rightTriggerDown;
    //左手柄扳机抬起
    public NoloRightHandle rightTriggerUp;
    //左手柄扳机持续按住
    public NoloRightHandle rightTriggerDownKeep;

    //左手柄系统按键（电源键）按下
    public NoloRightHandle rightSystemDown;
    //左手柄系统按键（电源键）抬起
    public NoloRightHandle rightSystemUp;
    public NoloRightHandle rightSystemDownKeep;

    //左手柄返回按键按下
    public NoloRightHandle rightBackDown;
    //左手柄返回按键抬起
    public NoloRightHandle rightBackUp;
    //左手柄返回按键持续按下
    public NoloRightHandle rightBackDownKeep;

    //左手柄音量减键按下
    public NoloRightHandle rightVolumeDownDown;
    //左手柄音量减键抬起
    public NoloRightHandle rightVolumeDownUp;
    //左手柄音量减键持续按下
    public NoloRightHandle rightVolumeDownDownKeep;

    //左手柄音量加键按下
    public NoloRightHandle rightVolumeUpDown;
    //左手柄音量加键抬起
    public NoloRightHandle rightVolumeUpUp;
    //左手柄音量加键持续按下
    public NoloRightHandle rightVolumeUpDownKeep;
    #endregion 

    #region 左手柄按键
    public void LeftHandle() 
    {

        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.TouchPad) || Input.GetKeyDown(KeyCode.A))
        {
            leftTouchPadDown?.Invoke();
            //Debug.Log("左手柄触摸板按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad) || Input.GetKey(KeyCode.A))
        {
            leftTouchPadDownKeep?.Invoke();
            //Debug.Log("左手柄触摸板持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.TouchPad) || Input.GetKeyUp(KeyCode.A))
        {
            leftTouchPadUp?.Invoke();
            //Debug.Log("左手柄触摸板抬起");
        }


        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.Trigger) || Input.GetKeyDown(KeyCode.S))
        {
            leftTriggerDown?.Invoke();
            //Debug.Log("左手柄扳机按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger) || Input.GetKey(KeyCode.S))
        {

            //NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).TriggerHapticPulse(100);
            leftTriggerDownKeep?.Invoke();
            //Debug.Log("左手柄扳机持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.Trigger) || Input.GetKeyUp(KeyCode.S))
        {

            leftTriggerUp?.Invoke();
            ReleaseLeftHandle();
            //Debug.Log("左手柄扳机抬起");
        }

        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.System) || Input.GetKeyDown(KeyCode.D))
        {
            leftSystemDown?.Invoke();
            //Debug.Log("左手柄系统电源键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.System) || Input.GetKey(KeyCode.D))
        {
            leftSystemDownKeep?.Invoke();
            //Debug.Log("左手柄系统电源键持续按下（不允许开发者使用该按键功能）");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.System) || Input.GetKeyUp(KeyCode.D))
        {
            leftSystemUp?.Invoke();
            //Debug.Log("左手柄系统电源键抬起");
        }

        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.Back) || Input.GetKeyDown(KeyCode.Escape))
        {
            leftBackDown?.Invoke();
            //Debug.Log("左手柄返回键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Back) || Input.GetKey(KeyCode.Escape))
        {
            leftBackDownKeep?.Invoke();
            //Debug.Log("左手柄返回键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.Back) || Input.GetKeyUp(KeyCode.Escape))
        {
            leftBackUp?.Invoke();
            //Debug.Log("左手柄返回键抬起");
        }



        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.VolumeDown))
        {
            leftVolumeDownDown?.Invoke();
            //Debug.Log("左手柄音量加键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeDown))
        {
            leftVolumeDownDownKeep?.Invoke();
            //Debug.Log("左手柄音量加键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.VolumeDown))
        {
            leftVolumeDownUp?.Invoke();
            //Debug.Log("左手柄音量加键抬起");
        }


        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.VolumeUp))
        {
            leftVolumeUpDown?.Invoke();
            //Debug.Log("左手柄音量减键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeUp))
        {
            leftVolumeUpDownKeep?.Invoke();
            //Debug.Log("左手柄音量减键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.VolumeUp))
        {
            leftVolumeUpUp?.Invoke();
            //Debug.Log("左手柄音量加键抬起");
        }
    }
    #endregion

    #region 右手柄按键
    public void RightHandle() 
    {
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.TouchPad))
        {
            rightTouchPadDown?.Invoke();
            //Debug.Log("右手柄触摸板按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad))
        {
            rightTouchPadDownKeep?.Invoke();
            //Debug.Log("右手柄触摸板持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.TouchPad))
        {
            rightTouchPadUp?.Invoke();
            //Debug.Log("右手柄触摸板抬起");
        }


        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Trigger))
        {
            rightTriggerDown?.Invoke();
            //Debug.Log("右手柄扳机按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger))
        {
            //NoloVR_Controller.GetDevice(NoloDeviceType.RightController).TriggerHapticPulse(100);
            rightTriggerDownKeep?.Invoke();
            //Debug.Log("右手柄扳机持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Trigger))
        {
            rightTriggerUp?.Invoke();
            ReleaseLeftHandle();
            //Debug.Log("右手柄扳机抬起");
        }



        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.System))
        {
            rightSystemDown?.Invoke();
            //Debug.Log("右手柄系统电源键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.System))
        {
            rightSystemDownKeep?.Invoke();
            //Debug.Log("右手柄系统电源键持续按下（不允许开发者使用该按键功能）");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.System))
        {
            rightSystemUp?.Invoke();
            //Debug.Log("右手柄系统电源键抬起");
        }



        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Back))
        {
            rightBackDown?.Invoke();
            //Debug.Log("右手柄返回键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Back))
        {
            rightBackDownKeep?.Invoke();
            //Debug.Log("右手柄返回键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Back))
        {
            rightBackUp?.Invoke();
            //Debug.Log("右手柄返回键抬起");
        }



        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.VolumeDown))
        {
            rightVolumeDownDown?.Invoke();
            //Debug.Log("右手柄音量加键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeDown))
        {
            rightVolumeDownDownKeep?.Invoke();
            //Debug.Log("右手柄音量加键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.VolumeDown))
        {
            rightVolumeDownUp?.Invoke();
            //Debug.Log("右手柄音量加键抬起");
        }


        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.VolumeUp))
        {
            rightVolumeUpDown?.Invoke();
            //Debug.Log("右手柄音量加键按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeUp))
        {
            rightVolumeUpDownKeep?.Invoke();
            //Debug.Log("右手柄音量减键持续按下");
        }
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.VolumeUp))
        {
            rightVolumeUpUp?.Invoke();
            //Debug.Log("右手柄音量加键抬起");
        }
    }
    #endregion

    #region 手柄Transform
    [Header("手柄Transform")]
    //左手柄
    public Transform leftHandle;
    //右手柄
    public Transform rightHandle;
    #endregion



    #region 手柄射线
    //左手柄
    public Ray LeftHandleRay()
    {
        return new Ray(leftHandle.position, leftHandle.forward);
    }

    //右手柄
    public Ray RightHandleRay()
    {
        return new Ray(rightHandle.position, rightHandle.forward);
    }
    #endregion

    #region 手柄当前抓取对着的物体
    //左手柄
    //左手柄在手柄按键按下时对着的物体
    public GameObject leftHandleObj=null;
    //左手柄按键时抓物
    public void LeftHandleGameObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(LeftHandleRay(), out hit))
        {
            leftHandleObj = hit.transform.gameObject;
            Debug.Log(hit.point + "发射线");
            //避免两个手柄抓住同一个物体
            if (rightHandleObj == leftHandleObj)
            {
                rightHandleObj = null;
            }
        }
    }
    //左手柄松开时
    public void ReleaseLeftHandle()
    {
        leftHandleObj = null;
    }


    //右手柄
    //右手柄在手柄按键按下时对着的物体
    public GameObject rightHandleObj = null;
    public void RightHandleGameObject() 
    {
        RaycastHit hit;
        if (Physics.Raycast(RightHandleRay(), out hit))
        {
            rightHandleObj = hit.transform.gameObject;
            //避免两个手柄抓住同一个物体
            if (leftHandleObj == rightHandleObj)
            {
                leftHandleObj = null;
            }
        }
    }

    //右柄松开时
    public void ReleaseRightHandle()
    {
        rightHandleObj = null;
    }
    #endregion

    #region 手柄光剑长短变化
   // public LineRenderer leftLaserRay,rightLaserRay;

    public void LeftLaserRayRenderer() 
    {
        //Ray ray = new Ray(leftLaserRay.transform.position, leftLaserRay.transform.forward);
        //RaycastHit hit;
        
        //if (Physics.Raycast(ray,out hit)) 
        //{
        //    leftHandleObj = hit.transform.gameObject;
        //    float value = (hit.point - transform.position).magnitude;
        //    //leftLaserRay.SetPosition(1,hit.point);
        //    leftLaserRay.SetPosition(1, transform.forward * value);
        //    Debug.DrawLine(leftLaserRay.transform.position, hit.point, Color.blue);
        //}
        //else 
        //{
        //    leftHandleObj = null;
        //    leftLaserRay.SetPosition(1, leftLaserRay.transform.forward*200);
        //    Debug.DrawLine(leftLaserRay.transform.position, leftLaserRay.transform.forward * 200, Color.white);
        //}
    }
    public void RightLaserRayRenderer()
    {
    //    Ray ray = new Ray(leftLaserRay.transform.position, rightLaserRay.transform.forward);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        rightHandleObj = hit.transform.gameObject;
    //        float value = (hit.point - transform.position).magnitude;
    //        rightLaserRay.SetPosition(1, transform.forward * value);
    //        //rightLaserRay.SetPosition(1, rightLaserRay.transform.forward*hit.distance);
    //    }
    //    else
    //    {
    //        rightHandleObj = null;
    //        rightLaserRay.SetPosition(1, rightLaserRay.transform.forward * 200);
    //    }
    }

    #endregion
}
