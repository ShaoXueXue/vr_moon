using UnityEngine;
using System.Collections;
using NoloVR;
public class NoloVR_Model : MonoBehaviour
{
    //[SerializeField]
    //private TrackedDevice trackedDevice = null;
    [SerializeField]
    private Transform touchpad=null;
    [SerializeField]
    private Transform menu=null;
    [SerializeField]
    private Transform system=null;
    [SerializeField]
    private Transform grip_left=null;
    [SerializeField]
    private Transform grip_right=null;
    [SerializeField]
    private Transform trigger=null;


    //// Use this for initialization
    //void OnEnable()
    //{
    //    trackedDevice = GetComponentInParent<NoloVR_TrackedDevice>();
    //    touchpad = transform.Find("buttons/button_touchpad");
    //    menu = transform.Find("buttons/button_menu");
    //    system = transform.Find("buttons/button_system");
    //    grip_left = transform.Find("buttons/button_grip_left");
    //    grip_right = transform.Find("buttons/button_grip_right");
    //    trigger = transform.Find("buttons/button_trigger");

    //}
    private void Update()
    {
#if NOLO_6DOF
        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.TouchPad))
        {
            TouchPad_Down();
        }
        else
        {
            TouchPad_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.Menu))
        {
            Menu_Down();
        }
        else
        {
            Menu_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.System))
        {
            System_Down();
        }
        else
        {
            System_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.Grip))
        {
            Grip_Down();
        }
        else
        {
            Grip_Up();
        }

        if (NoloVR_Controller.GetDevice(trackedDevice).GetNoloButtonPressed(NoloButtonID.Trigger))
        {
            Trigger_Down();
        }
        else
        {
            Trigger_Up();
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

    //menu
    private void Menu_Down()
    {
        menu.transform.localPosition = new Vector3(0, -1, 0);
    }

    private void Menu_Up()
    {
        menu.transform.localPosition = Vector3.zero;
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
        trigger.transform.localPosition = new Vector3(0, 12, -5);
        trigger.transform.localRotation = Quaternion.Euler(-20, 0, 0);
    }

    private void Trigger_Up()
    {
        trigger.transform.localPosition = Vector3.zero;
        trigger.transform.localRotation = Quaternion.identity;
    }

    //grip
    private void Grip_Down()
    {
        grip_left.transform.localPosition = new Vector3(1, 0, 0);
        grip_right.transform.localPosition = new Vector3(-1, 0, 0);
    }

    private void Grip_Up()
    {
        grip_left.transform.localPosition = Vector3.zero;
        grip_right.transform.localPosition = Vector3.zero;
    }

}
