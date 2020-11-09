using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoloVR
{
    public class TrackedDevice : MonoBehaviour
    {
        [SerializeField]
        private NoloDeviceType m_DeviceType= NoloDeviceType.LeftController;
        public NoloDeviceType DeviceType => m_DeviceType;

        private Quaternion inputQuat = Quaternion.identity;
        private Quaternion correctQuat = Quaternion.identity;
        public Quaternion OffsetQuat { private get; set; }

        public Vector3 Forward { get { return transform.forward; } }

        private GameObject vrCamera=null;
        private void Awake()
        {
            vrCamera = NoloVR_System.GetInstance().VRCamera;
        }

        private void Update()
        {
            if (NoloVR_Playform.GetInstance().GetPlayformError() != NoloError.None)
                return;
            UpdatePose();
        }

        private void UpdatePose()
        {
            var pose = NoloVR_Controller.GetDevice(m_DeviceType).GetPose();
            if (m_DeviceType != NoloDeviceType.Hmd)
            {
                if (NoloVR_System.GetInstance().trackModel == NoloVR_Manager.TrackModel.Track_3dof)
                {
                    //如果真实的设备是3dof，采用默认高度
                    //如果真实的设备是6dof，要采用定位数据
                    if (NoloVR_System.GetInstance().realTrackDevices == 3)
                        UpdateRotation(pose.rot);
                    else
                        transform.localRotation = pose.rot;
                }
                else
                    UpdateRotation(pose.rot);
            }
            else
            {
                if (NoloVR_System.GetInstance().trackModel == NoloVR_Manager.TrackModel.Track_3dof)
                {
                    //如果真实的设备是3dof，采用默认高度
                    //如果真实的设备是6dof，要采用定位数据
                    if (NoloVR_System.GetInstance().realTrackDevices == 3)
                        transform.localPosition = pose.pos + new Vector3(0, NoloVR_System.GetInstance().defaultHeight, 0);
                    else
                        transform.localPosition = pose.pos;
                }
                else
                {
                    if (vrCamera == null)
                    {
                        Debug.LogError("Not find your vr camera");
                        return;
                    }
                    var cameraLoaclPosition = transform.localRotation * vrCamera.transform.localPosition;
                    transform.localPosition = pose.pos - cameraLoaclPosition;
                }
            }
        }
        public void UpdateRotation(Quaternion rot)
        {
            inputQuat = rot;
#if UNITY_EDITOR
            transform.localRotation = OffsetQuat * correctQuat * rot;
#else
            transform.localRotation = OffsetQuat * rot;
#endif

        }
        //矫正方向
        public void ResetThisHand()
        {
            correctQuat = Quaternion.Inverse(inputQuat);
        }
    }
}

