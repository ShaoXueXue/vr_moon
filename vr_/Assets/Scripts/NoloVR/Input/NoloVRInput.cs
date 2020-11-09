using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoloVR
{
    public class NoloVRInput : MonoBehaviour
    {
        private static NoloVRInput _singleton;
        public static NoloVRInput singleton
        {
            get
            {
                return _singleton;
            }
        }
        private void Awake()
        {
            _singleton = this;
        }

        //委托方法
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


        //private void Update()
        //{
        //   Debug.Log("左手电量"+NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloDeviceElectricity());
        //    Debug.Log("右手电量" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloDeviceElectricity());
        //}

        #region .Input
        #region .Left
        /// <summary>
        /// 左手按键按下
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloLeftButtonDown(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonDown(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        /// <summary>
        /// 左手按键持续按下
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloLeftButtonPressed(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        /// <summary>
        /// 左手按键抬起
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloLeftButtonUp(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        #endregion
        #region .Right
        /// <summary>
        /// 右手按键按下
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloRightButtonDown(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonDown(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        /// <summary>
        /// 右手按键持续按下
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloRightButtonPressed(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        /// <summary>
        /// 右手按键持续抬起
        /// </summary>
        /// <param name="var">按键类型</param>
        /// <returns></returns>
        public bool OnNoloRightButtonUp(NoloButtonID var)
        {
            bool state = false;
            switch (var)
            {
                case NoloButtonID.Trigger:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Trigger);
                    break;
                case NoloButtonID.TouchPad:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.TouchPad);
                    break;
                case NoloButtonID.System:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.System);
                    break;
                case NoloButtonID.SystemLongPress:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.SystemLongPress);
                    break;
                case NoloButtonID.Back:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.Back);
                    break;
                case NoloButtonID.VolumeDown:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.VolumeDown);
                    break;
                case NoloButtonID.VolumeUp:
                    state = NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.VolumeUp);
                    break;
                default:
                    break;
            }
            return state;
        }
        #endregion
        #endregion
        #region .Touch

        public bool OnNoloLeftTouchDown()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloTouchDown(NoloTouchID.TouchPad);
        }

        public bool OnNoloLeftTouchPressed()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloTouchPressed(NoloTouchID.TouchPad);
        }
        public bool OnNoloLeftTouchUp()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloTouchUp(NoloTouchID.TouchPad);
        }

        public bool OnNoloRightTouchDown()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloTouchDown(NoloTouchID.TouchPad);
        }

        public bool OnNoloRightTouchPressed()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloTouchPressed(NoloTouchID.TouchPad);
        }
        public bool OnNoloRightTouchUp()
        {
            return NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloTouchUp(NoloTouchID.TouchPad);
        }
        #endregion
        #region 震动
        public void OnNoloLeftHapticPulse(int value)
        {
            //NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).TriggerHapticPulse(value);
        }
        public void OnNoloRightHapticPulse(int value)
        {
            //NoloVR_Controller.GetDevice(NoloDeviceType.RightController).TriggerHapticPulse(value);
        }
        #endregion
        #region 获取Touch轴向值
        public Vector2 OnLeftDeltaAxis()
        {
            //Debug.Log(NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetDeltaAxis() + "：左手 +2");
            return NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetDeltaAxis();

        }
        public Vector2 OnRightDeltaAxis()
        {
            //Debug.Log(NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetDeltaAxis() + "：右手 +2");
            return NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetDeltaAxis();
        }
        #endregion
    }
}

