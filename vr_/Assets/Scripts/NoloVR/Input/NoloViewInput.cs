using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoloVR
{
    public class NoloViewInput : MonoBehaviour
    {
        public static TrackedDevice Tracker_L { get; private set; }
        public static TrackedDevice Tracker_R { get; private set; }
        public Transform CamTrans => transform;

        public void Awake()
        {
            Initial();
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
        private void Update()
        {
            OnRecenter();
            OnViewRotate();
            FieldOfView();
        }
        #region  .矫正手柄
        private float recenter_PreTime_L = 0;
        private float recenter_PreTime_R = 0;
        private float recenterSpacingTime = 0.5f;

        private void OnRecenter()
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.System))  //system = menu
            {
                //Debug.Log("矫正");
                if (Time.time - recenter_PreTime_L <= recenterSpacingTime)
                {
                    Tracker_L.ResetThisHand();
                    recenter_PreTime_L = 0;
                }
                else
                    recenter_PreTime_L = Time.time;
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.System))
            {
                if (Time.time - recenter_PreTime_R <= recenterSpacingTime)
                {
                    Tracker_R.ResetThisHand();
                    recenter_PreTime_R = 0;
                }
                else
                    recenter_PreTime_R = Time.time;
            }
        }
        #endregion
        #region .视野旋转
        private enum TouchPhase { None, Left, Right }
        private TouchPhase m_TouchPhase = TouchPhase.None;
        private const float m_AxisFactor_Nolo = 0.05f;

        private void OnViewRotate()
        {
            if (NoloHandleRay.singleton.rightHandleObj != null || NoloHandleRay.singleton.leftHandleObj != null)
            {
                if (NoloHandleRay.singleton.rightHandleObj != null)
                {
                    //Debug.Log("右手包含物体，跳出");
                }
                else if (NoloHandleRay.singleton.leftHandleObj != null)
                {
                    //Debug.Log("左手包含物体，跳出");
                }
                return;
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.TouchPad) || NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.TouchPad))    //点击Touch
            {
                if (m_TouchPhase != TouchPhase.None)
                {
                    m_TouchPhase = TouchPhase.None;
                    ViewAxisInput.Instance.EndAxis_Input();
                }
                if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.TouchPad))
                {
                    //Debug.Log("左圆盘为点击，不是触摸，跳出");
                }
                else if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.TouchPad))
                {
                    //Debug.Log("右圆盘为点击，不是触摸，跳出");
                }
                return;
            }
            switch (m_TouchPhase)
            {
                case TouchPhase.None:
                    if (NoloVRInput.singleton.OnNoloLeftTouchDown())
                    {
                        m_TouchPhase = TouchPhase.Left;
                        ViewAxisInput.Instance.StartAxis_Input();
                    }
                    else if (NoloVRInput.singleton.OnNoloRightTouchDown())
                    {
                        m_TouchPhase = TouchPhase.Right;
                        ViewAxisInput.Instance.StartAxis_Input();
                    }
                    break;
                case TouchPhase.Left:
                    if (NoloVRInput.singleton.OnNoloLeftTouchUp())
                    {
                        m_TouchPhase = TouchPhase.None;
                        ViewAxisInput.Instance.EndAxis_Input();
                    }
                    else 
                    {
                        //Debug.Log("左手条件成熟触发旋转 + 1");
                        ViewAxisInput.Instance.ProcessAxis_Input(NoloVRInput.singleton.OnLeftDeltaAxis() * m_AxisFactor_Nolo);
                    }
                    break;
                case TouchPhase.Right:
                    if (NoloVRInput.singleton.OnNoloRightTouchUp())
                    {
                        m_TouchPhase = TouchPhase.None;
                        ViewAxisInput.Instance.EndAxis_Input();
                    }
                    else
                    {
                        //Debug.Log("右手条件成熟触发旋转 + 1");
                        ViewAxisInput.Instance.ProcessAxis_Input(NoloVRInput.singleton.OnRightDeltaAxis() * m_AxisFactor_Nolo);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
        #region .视野缩放/视野移动
        private const float m_PanFactor_Nolo = 20f;
        private bool isMove_Left;
        private bool isMove_Right;
        private Vector3 m_OldForward_L;
        private Vector3 m_OldForward_R;
        private float m_OldDotRR;   //dot between RightHand & Right Axis
        private float m_OldDotLL;   //dot between LightHand & Light Axis
        private const float m_ZoomFactor_Nolo = 0.4f;

        private void FieldOfView()
        {
            if (NoloHandleRay.singleton.rightHandleObj != null || NoloHandleRay.singleton.leftHandleObj != null)
                return;
            bool triggerDown_Left = NoloVRInput.singleton.OnNoloLeftButtonDown(NoloButtonID.TouchPad);
            bool triggerPress_Left = NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.TouchPad);
            bool triggerUp_Left = NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.TouchPad);
            bool triggerDown_Right = NoloVRInput.singleton.OnNoloRightButtonDown(NoloButtonID.TouchPad);
            bool triggerPress_Right = NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.TouchPad);
            bool triggerUp_Right = NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.TouchPad);

            bool isTrigger_L = NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger);
            bool isTrigger_R = NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger);

            if (triggerUp_Left || triggerUp_Right)
            {
                isMove_Left = isMove_Right = false;
            }
                
            if ((triggerDown_Left && triggerDown_Right) || (triggerPress_Left && triggerDown_Right) || (triggerDown_Left && triggerPress_Right))
            {
                Vector3 rightDir = MirrorFlipCamera.IsFlipX ? -CamTrans.right : CamTrans.right;
                m_OldDotRR = Vector3.Dot(rightDir, Tracker_R.Forward);
                m_OldDotLL = Vector3.Dot(-rightDir, Tracker_L.Forward);
                isMove_Left = isMove_Right = false;
            }
            else if(triggerPress_Left && triggerPress_Right)
            {
                //Debug.Log("视野缩放");
                Vector3 rightDir = MirrorFlipCamera.IsFlipX ? -CamTrans.right : CamTrans.right;
                float dotRR = Vector3.Dot(rightDir, Tracker_R.Forward);
                float dotLL = Vector3.Dot(-rightDir, Tracker_L.Forward);
                float deltaAngleLL = Mathf.Acos(dotLL) - Mathf.Acos(m_OldDotLL);
                float deltaAngleRR = Mathf.Acos(dotRR) - Mathf.Acos(m_OldDotRR);
                m_OldDotRR = dotRR;
                m_OldDotLL = dotLL;
                if(ViewNodeManager.singleton != null)
                    ViewNodeManager.singleton.OnHoldZoom((deltaAngleLL + deltaAngleRR) * m_ZoomFactor_Nolo);
                //NoloVRInput.singleton.OnNoloLeftHapticPulse(100);
                //NoloVRInput.singleton.OnNoloRightHapticPulse(100);
                isMove_Left = isMove_Right = false;
            }
            if(triggerUp_Left && triggerUp_Right)
            {
                //NoloVRInput.singleton.OnNoloLeftHapticPulse(0);
                //NoloVRInput.singleton.OnNoloRightHapticPulse(0);
            }

            if(triggerDown_Left && !triggerPress_Right)
            {
                m_OldForward_L = Tracker_L.Forward;
                isMove_Left = true;
            }
            else if(triggerDown_Right && !triggerPress_Left)
            {
                m_OldForward_R = Tracker_R.Forward;
                isMove_Right = true;
            }
            if (triggerUp_Right || triggerUp_Left)
                isMove_Left = isMove_Right = false;
            if(isMove_Left && !triggerDown_Left && !isTrigger_L)
            {
                PanInput(Tracker_L.Forward - m_OldForward_L);
                m_OldForward_L = Tracker_L.Forward;
            }
            else if (isMove_Right && !triggerDown_Right && !isTrigger_R) //Move_R Keep
            {
                PanInput(Tracker_R.Forward - m_OldForward_R);
                m_OldForward_R = Tracker_R.Forward;
            }
        }
        private void PanInput(Vector3 deltaVector)
        {
            Vector3 deltaProject = Vector3.ProjectOnPlane(deltaVector, Vector3.forward);
            if (ViewNodeManager.singleton != null)
                ViewNodeManager.singleton.Pan_Input(deltaProject * m_PanFactor_Nolo);
        }
        #endregion
    }
}

