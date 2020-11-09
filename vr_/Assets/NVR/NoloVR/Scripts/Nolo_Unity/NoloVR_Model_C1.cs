using UnityEngine;
using System.Collections;
using NoloVR;
public class NoloVR_Model_C1 : MonoBehaviour
{
    [SerializeField]
    private TrackedDevice trackedDevice=null;
    [SerializeField]
    private Transform touchpad=null;
    [SerializeField]
    private Transform back=null;
    [SerializeField]
    private Transform system=null;
    [SerializeField]
    private Transform trigger=null;
    [SerializeField]
    private Transform volume=null;

    // Use this for initialization
    //void OnEnable()
    //{
    //    trackedDevice = GetComponentInParent<NoloVR_TrackedDevice>();
    //    touchpad = transform.Find("buttons/button_touchpad");
    //    back = transform.Find("buttons/button_back");
    //    system = transform.Find("buttons/button_system");
    //    trigger = transform.Find("buttons/button_trigger");
    //    volume = transform.Find("buttons/button_volume");

    //}
    private void Update()
    {
#if NOLO_3DOF
        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.TouchPad))
        {
            TouchPad_Down();
        }
        else
        {
            TouchPad_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.Back))
        {
            Back_Down();
        }
        else
        {
            Back_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.System))
        {
            System_Down();
        }
        else
        {
            System_Up();
        }
        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.Trigger))
        {
            Trigger_Down();
        }
        else
        {
            Trigger_Up();
        }
        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.VolumeDown))
        {
            VolumeDown_Down();
        }
        else if(NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.VolumeUp))
        {
            VolumeUp_Down();
        }
        else
        {
            Volume_Up();
        }

#endif
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
