/*************************************************************
 * 
 *  Copyright(c) 2017 Lyrobotix.Co.Ltd.All rights reserved.
 *  NoloVR_Manager.cs
 *   
*************************************************************/

using NoloClientCSharp;
using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class NoloVR_Manager : MonoBehaviour
{
    public string appKey;
    public GameObject VRCamera;
    public TrackModel gameTrackModel;
    public TurnAroundButtonType turnAroundButtonType;
    public bool useDefaultHeight = true;
    [Range(1, 2)]
    public float defaultHeight = 1.7f;

    [HideInInspector]
    public NoloVR_TrackedDevice[] objects;

    public enum TrackModel
    {
        Track_3dof,//3dof 游戏模式
        Track_6dof,//6dof 游戏模式
    }
    private void Awake()
    {
        NoloVR_System.GetInstance().objects = GameObject.FindObjectsOfType<NoloVR_TrackedDevice>();
        NoloVR_System.GetInstance().VRCamera = this.VRCamera;
        NoloVR_System.GetInstance().trackModel = gameTrackModel;
        if (useDefaultHeight)
        {
            NoloVR_System.GetInstance().defaultHeight = defaultHeight;
        }
        else
        {
            NoloVR_System.GetInstance().defaultHeight = 0;
        }
#if NOLO_3DOF
        Debug.Log("3dof");
#elif NOLO_6DOF
        Debug.Log("6dof");
#else
         Debug.Log("other");
#endif
    }

    private void Start()
    {
        NoloVR_Playform.GetInstance().Authentication(appKey);
    }
    public void OnClickButton()
    {
        //NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).TriggerHapticPulse(100);
        Debug.Log("LeftController Trigger Pressed");
    }
    private void Update()
    {

#if NOLO_6DOF
        if (turnAroundButtonType!= TurnAroundButtonType.Null)
        {
            TurnAroundEventsMonitor();
        }
#endif
        Recenter();
    }

    //turn around about
    private int leftcontrollerTurn_PreFrame = -1;
    private int rightcontrollerTurn_PreFrame = -1;
    private int turnAroundSpacingFrame = 20;
    private void TurnAroundEventsMonitor()
    {
        //leftcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp((uint)1 << (int)turnAroundButtonType))
        {
            if (Time.frameCount - leftcontrollerTurn_PreFrame <= turnAroundSpacingFrame)
            {
                NOLO_Events.Send(NOLO_Events.EventsType.TurnAround);
                leftcontrollerTurn_PreFrame = -1;
            }
            else
            {
                leftcontrollerTurn_PreFrame = Time.frameCount;
            }
        }
        //rightcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp((uint)1 << (int)turnAroundButtonType))
        {
            if (Time.frameCount - rightcontrollerTurn_PreFrame <= turnAroundSpacingFrame)
            {
                NOLO_Events.Send(NOLO_Events.EventsType.TurnAround);
                rightcontrollerTurn_PreFrame = -1;
            }
            else
            {
                rightcontrollerTurn_PreFrame = Time.frameCount;
            }
        }
    }
    //recenter about
#if NOLO_6DOF
    private int leftcontrollerRecenter_PreFrame = -1;
    private int rightcontrollerRecenter_PreFrame = -1;
    private int recenterSpacingFrame = 20;
#endif

    private void Recenter()
    {
#if NOLO_6DOF
        //leftcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.System))
        {
            if (Time.frameCount - leftcontrollerRecenter_PreFrame <= recenterSpacingFrame)
            {
                UnityEngine.XR.InputTracking.Recenter();
                NOLO_Events.Send(NOLO_Events.EventsType.RecenterLeft);
                leftcontrollerRecenter_PreFrame = -1;
            }
            else
            {
                leftcontrollerRecenter_PreFrame = Time.frameCount;
            } 
        }
        //rightcontroller double click system button
        if (NoloVR_Controller.GetDevice(NoloDeviceType.RightController).GetNoloButtonUp(NoloButtonID.System))
        {
            if (Time.frameCount - rightcontrollerRecenter_PreFrame <= recenterSpacingFrame)
            {
                UnityEngine.XR.InputTracking.Recenter();
                NOLO_Events.Send(NOLO_Events.EventsType.RecenterRight);
                rightcontrollerRecenter_PreFrame = -1;
            }
            else
            {
                rightcontrollerRecenter_PreFrame = Time.frameCount;
            }
        }
#elif NOLO_3DOF
        if (NoloVR_Controller.GetDevice(NoloDeviceType.LeftController).GetNoloButtonUp(NoloButtonID.SystemLongPress))
        {
            UnityEngine.XR.InputTracking.Recenter();
        }
#endif
    }

    private void OnApplicationQuit()
    {
        //close connect from device
        Debug.Log("Nolo debug:Application quit");
        NoloVR_Playform.GetInstance().DisconnectDevice();
    }
}
