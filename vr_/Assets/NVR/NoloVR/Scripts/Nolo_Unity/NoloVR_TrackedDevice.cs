/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_TrackedDevice.cs
 *   
*************************************************************/

using UnityEngine;

public class NoloVR_TrackedDevice : MonoBehaviour
{

    public NoloDeviceType deviceType;
    private GameObject vrCamera;

#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Start()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        vrCamera = NoloVR_System.GetInstance().VRCamera;
    }

#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Update()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        if (NoloVR_Playform.GetInstance().GetPlayformError() != NoloError.None)
        {
            return;
        }
        UpdatePose();
    }

    private void UpdatePose()
    {
        var pose = NoloVR_Controller.GetDevice(deviceType).GetPose();

        if (deviceType != NoloDeviceType.Hmd)
        {
            if(NoloVR_System.GetInstance().trackModel == NoloVR_Manager.TrackModel.Track_3dof)
            {
                //如果真实的设备是3dof，采用默认高度
                //如果真实的设备是6dof，要采用定位数据
                if(NoloVR_System.GetInstance().realTrackDevices == 3)
                {
                    transform.localPosition = pose.pos + new Vector3(0, NoloVR_System.GetInstance().defaultHeight, 0);
                    transform.localRotation = pose.rot;
                }
                else
                {
                    transform.localPosition = pose.pos;
                    transform.localRotation = pose.rot;
                }
               
            }
            else
            {
                transform.localPosition = pose.pos;
                transform.localRotation = pose.rot;
            }
        }
        else
        {
            if (NoloVR_System.GetInstance().trackModel == NoloVR_Manager.TrackModel.Track_3dof)
            {
                //如果真实的设备是3dof，采用默认高度
                //如果真实的设备是6dof，要采用定位数据
                if(NoloVR_System.GetInstance().realTrackDevices == 3)
                {
                    transform.localPosition = pose.pos + new Vector3(0, NoloVR_System.GetInstance().defaultHeight, 0);
                }
                else
                {
                    transform.localPosition = pose.pos;
                }
            }
            else
            {
                if (vrCamera == null)
                {
                    Debug.LogError("Not find your vr camera");
                    return;
                }
                //transform.localRotation = pose.rot;
                var cameraLoaclPosition = transform.localRotation * vrCamera.transform.localPosition;
                transform.localPosition = pose.pos - cameraLoaclPosition;
            }
        }
    }
}
