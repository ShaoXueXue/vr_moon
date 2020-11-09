using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;
using NoloVR;

/// <summary>
/// 手柄按键效果
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField]
    NoloDeviceType deviceType;  

    [SerializeField]
    private Transform touchpad = null;
    [SerializeField]
    private Transform back = null;
    [SerializeField]
    private Transform system = null;
    [SerializeField]
    private Transform trigger = null;
    [SerializeField]
    private Transform volume = null;

    private void Update()
    {
        if(deviceType == NoloDeviceType.LeftController)
        {
            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.TouchPad))
            {
                TouchPad_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.TouchPad))
            {
                TouchPad_Up();
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Back))
            {
                Back_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Back))
            {
                Back_Up();
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.System))
            {
                System_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.System))
            {
                System_Up();
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.Trigger))
            {
                Trigger_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.Trigger))
            {
                Trigger_Up();
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.VolumeDown))
            {
                VolumeDown_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.VolumeDown))
            {
                Volume_Up();
            }

            if (NoloVRInput.singleton.OnNoloLeftButtonPressed(NoloButtonID.VolumeUp))
            {
                VolumeDown_Down();
            }
            if (NoloVRInput.singleton.OnNoloLeftButtonUp(NoloButtonID.VolumeUp))
            {
                Volume_Up();
            }
        }
        else if(deviceType == NoloDeviceType.RightController)
        {
            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.TouchPad))
            {
                TouchPad_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.TouchPad))
            {
                TouchPad_Up();
            }

            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Back))
            {
                Back_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Back))
            {
                Back_Up();
            }

            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.System))
            {
                System_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.System))
            {
                System_Up();
            }

            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.Trigger))
            {
                Trigger_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.Trigger))
            {
                Trigger_Up();
            }

            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.VolumeDown))
            {
                VolumeDown_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.VolumeDown))
            {
                Volume_Up();
            }

            if (NoloVRInput.singleton.OnNoloRightButtonPressed(NoloButtonID.VolumeUp))
            {
                VolumeDown_Down();
            }
            if (NoloVRInput.singleton.OnNoloRightButtonUp(NoloButtonID.VolumeUp))
            {
                Volume_Up();
            }
        }
    }

    //touchpad
    private void TouchPad_Down()
    {
        touchpad.transform.localPosition = new Vector3(0, -1, 0);
    }
    private void TouchPad_Up()
    {
        touchpad.transform.localPosition = Vector3.zero;
    }
    //back
    private void Back_Down()
    {
        back.transform.localPosition = new Vector3(0, -1, 0);
    }

    private void Back_Up()
    {
        back.transform.localPosition = Vector3.zero;
    }

    //system
    private void System_Down()
    {
        system.transform.localPosition = new Vector3(0, -1, 0);
    }

    private void System_Up()
    {
        system.transform.localPosition = Vector3.zero;
    }

    //trigger
    private void Trigger_Down()
    {
        trigger.transform.localPosition = new Vector3(0.2f, 2.6f, 0.2f);
        trigger.transform.localRotation = Quaternion.Euler(0, 0, -5);
    }

    private void Trigger_Up()
    {
        trigger.transform.localPosition = Vector3.zero;
        trigger.transform.localRotation = Quaternion.identity;
    }

    //volume
    private void VolumeDown_Down()
    {
        volume.transform.localPosition = Vector3.zero;
        volume.transform.localRotation = Quaternion.Euler(0, -2, 0);
    }

    private void VolumeUp_Down()
    {
        volume.transform.localPosition = Vector3.zero;
        volume.transform.localRotation = Quaternion.Euler(0, 2, 0);
    }

    private void Volume_Up()
    {
        volume.transform.localPosition = Vector3.zero;
        volume.transform.localRotation = Quaternion.identity;
    }
}
