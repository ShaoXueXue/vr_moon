using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using NoloClientCSharp;


public class UI_Test : MonoBehaviour
{
    private Text UIText;

    private void Start()
    {
        UIText = GetComponent<Text>();
    }

    private void Update()
    {
        //Debug.Log(NoloVR_Plugins.GetElectricity(3));
        try
        {
            UIText.text = "DATA:" + "\n"
#if NOLO_6DOF
            + "HMD POS       :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().pos + "HMD ROT       :" + NoloVR_Plugins.GetPose(0).rot + "\n"
            + "HMD VEC     :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().vecVelocity  + "HMD ANGULAR     :" + NoloVR_Controller.GetDevice(NoloDeviceType.Hmd).GetPose().vecAngularVelocity + "\n"
            + "LEFT Touch AXIS     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetAxis(NoloTouchID.TouchPad) + "RIGHT Touch AXIS    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis(NoloTouchID.TouchPad) + "\n"
             + "LEFT Trigger AXIS     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetAxis(NoloTouchID.Trigger) + "RIGHT Trigger AXIS    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis(NoloTouchID.Trigger) + "\n"
            + "LEFT POS      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().pos + "RIGHT POS     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().pos + "\n"
            + "LEFT ROT      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().rot + "RIGHT ROT     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().rot + "\n"
            + "LEFT VEC      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().vecVelocity + "RIGHT VEC      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().vecVelocity + "\n"
             + "LEFT ANGULAR      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().vecAngularVelocity + "RIGHT ANGULAR      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().vecAngularVelocity + "\n"
            + "LEFT TRIGGER  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger) + "RIGHT TRIGGER :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger) + "\n"
            + "LEFT MENU     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Menu) + "RIGHT MENU    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Menu) + "\n"
            + "LEFT TOUCHPAD :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "RIGHT TOUCHPAD:" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "\n"
            + "LEFT SYSTEM   :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.System) + "RIGHT SYSTEM  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.System) + "\n"
            + "LEFT GRIP     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Grip) + "RIGHT GRIP    :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Grip) + "\n"
            //+ "LEFT up     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadUp) + "RIGHT up     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadUp) + "\n"
            //+ "LEFT down     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadDown) + "RIGHT down     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadDown) + "\n"
            //+ "LEFT left     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadLeft) + "RIGHT left     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadLeft) + "\n"
            //+ "LEFT right     :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPadRight) + "RIGHT right     :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPadRight) + "\n"
            + "HMD ELE      :" + NoloVR_Plugins.GetElectricity(0) + "BASE ELE     :" + NoloVR_Plugins.GetElectricity(3) + "\n"
            + "LEFT ELE      :" + NoloVR_Plugins.GetElectricity(1) + "RIGHT ELE     :" + NoloVR_Plugins.GetElectricity(2) + "\n"
#elif NOLO_3DOF
               + "LEFT POS      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().pos + "\n"
                + "LEFT ROT      :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetPose().rot + "\n"
                + "LEFT Touchpad  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "\n"
                + "LEFT Trigger  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger) + "\n"
                + "LEFT system  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.System) + "\n"
                + "LEFT systemLongPress  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.SystemLongPress) + "\n"
                + "LEFT back  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Back) + "\n"
                + "LEFT volimedown  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeDown) + "\n"
                + "LEFT volimeup  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.VolumeUp) + "\n"
                + "LEFT Axis  :" + NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetAxis(NoloTouchID.TouchPad) + "\n"
                 + "RIGHT POS      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().pos + "\n"
                + "RIGHT ROT      :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetPose().rot + "\n"
                + "RIGHT Touchpad  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.TouchPad) + "\n"
                + "RIGHT Trigger  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger) + "\n"
                + "RIGHT system  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.System) + "\n"
                + "RIGHT systemLongPress  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.SystemLongPress) + "\n"
                + "RIGHT back  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Back) + "\n"
                + "RIGHT volimedown  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeDown) + "\n"
                + "RIGHT volimeup  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.VolumeUp) + "\n"
                + "RIGHT Axis  :" + NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetAxis(NoloTouchID.TouchPad) + "\n"
#endif
            ;
        }
        catch (System.Exception e)
        {
            Debug.Log("Catch" + e.Message);
            throw;
        }
        //if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonPressed(NoloButtonID.Trigger))
        //{
        //    NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).TriggerHapticPulse(100);
        //}
        //if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonPressed(NoloButtonID.Trigger))
        //{
        //    NoloVR_Controller.GetDevice(NoloDeviceType.RightController).TriggerHapticPulse(100);
        //}
    }
}
