using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoloVR
{
    public class NoloControlInput : MonoBehaviour
    {
        public static NoloControlInput Instance;
        //双击的时间间隔，小于这个时间间隔才算双击
        private float spacingTime = 0.5f;
        public static TrackedDevice Tracker_L { get; private set; }
        public static TrackedDevice Tracker_R { get; private set; }

        //手柄控制的委托代理
        public delegate void ControlDelegate(NoloDeviceType handleType);

        //双击触摸板
        public ControlDelegate LeftTouchPadDoubleClick;
        public ControlDelegate RightTouchPadDoubleClick;
        //单击触摸板
        public ControlDelegate LeftTouchPadClick;
        public ControlDelegate RightTouchPadClick;

        //双击系统键（⭕键）
        public ControlDelegate LeftSystemDoubleClick;
        public ControlDelegate RightSystemDoubleClick;
        //单击系统键（⭕键）
        public ControlDelegate LeftSystemClick;
        public ControlDelegate RightSystemClick;


        //双击返沪键（▲键）
        public ControlDelegate LeftBackDoubleClick;
        public ControlDelegate RightBackDoubleClick;
        //单击返回键（▲键）
        public ControlDelegate LeftBackClick;
        public ControlDelegate RightBackClick;

        public void Awake()
        {
            Instance = this;
            Initial();
        }
        private void Update()
        {
            DoubleClickTouchPad();
            ClickTouchPad();
        }
        public void Initial()
        {
            foreach (TrackedDevice tracker in GetComponentsInChildren<TrackedDevice>())
            {
                if (tracker.DeviceType == NoloDeviceType.LeftController)
                    Tracker_L = tracker;
                else if (tracker.DeviceType == NoloDeviceType.RightController)
                    Tracker_R = tracker;
            }
        }

        //单击触摸板
        public void ClickTouchPad()
        {
            //左手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.TouchPad))
            {
                LeftTouchPadClick?.Invoke(NoloDeviceType.LeftController);
            }
            //右手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.TouchPad))
            {
                RightTouchPadClick?.Invoke(NoloDeviceType.RightController);
            }
        }

        private float touchPad_PreTime_L = 0;
        private float touchPad_PreTime_R = 0;
        //双击触摸板
        public void DoubleClickTouchPad()
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.TouchPad))  //system = menu
            {
                //Debug.Log("双击触摸板");
                if (Time.time - touchPad_PreTime_L <= spacingTime)
                {
                    LeftTouchPadDoubleClick?.Invoke(NoloDeviceType.LeftController);
                    touchPad_PreTime_L = 0;
                }
                else
                {
                    touchPad_PreTime_L = Time.time;
                }

            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.TouchPad))
            {
                if (Time.time - touchPad_PreTime_R <= spacingTime)
                {
                    RightTouchPadDoubleClick?.Invoke(NoloDeviceType.RightController);
                    touchPad_PreTime_R = 0;
                }
                else
                {
                    touchPad_PreTime_R = Time.time;
                }
            }
        }

        //单击系统键
        public void ClickSystem() 
        {
            //左手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.System))
            {
                LeftSystemClick?.Invoke(NoloDeviceType.LeftController);
            }
            //右手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.System))
            {
                RightSystemClick?.Invoke(NoloDeviceType.RightController);
            }
        }

        private float system_PreTime_L = 0;
        private float system_PreTime_R = 0;
        //双击系统键
        public void DoubleClickSystem() 
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.System))  //system = menu
            {
                //Debug.Log("双击触摸板");
                if (Time.time - system_PreTime_L <= spacingTime)
                {
                    LeftSystemDoubleClick?.Invoke(NoloDeviceType.LeftController);
                    system_PreTime_L = 0;
                }
                else
                {
                    system_PreTime_L = Time.time;
                }

            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.System))
            {
                if (Time.time - system_PreTime_R <= spacingTime)
                {
                    RightSystemDoubleClick?.Invoke(NoloDeviceType.RightController);
                    system_PreTime_R = 0;
                }
                else
                {
                    system_PreTime_R = Time.time;
                }
            }
        }

        //单击返回键
        public void ClickBack()
        {
            //左手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Back))
            {
                LeftBackClick?.Invoke(NoloDeviceType.LeftController);
            }
            //右手柄触摸板抬起
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Back))
            {
                LeftBackClick?.Invoke(NoloDeviceType.RightController);
            }
        }

        private float back_PreTime_L = 0;
        private float back_PreTime_R = 0;
        //双击返回键
        public void DoubleClickBack()
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.System))  //system = menu
            {
                //Debug.Log("双击触摸板");
                if (Time.time - back_PreTime_L <= spacingTime)
                {
                    LeftBackDoubleClick?.Invoke(NoloDeviceType.LeftController);
                    back_PreTime_L = 0;
                }
                else
                {
                    back_PreTime_L = Time.time;
                }

            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.System))
            {
                if (Time.time - back_PreTime_R <= spacingTime)
                {
                    RightBackDoubleClick?.Invoke(NoloDeviceType.RightController);
                    back_PreTime_R = 0;
                }
                else
                {
                    back_PreTime_R = Time.time;
                }
            }
        }

    }
}

