using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可拾取物体-cube  2d
/// </summary>
public class DemeCube : GraspingObjectBase2D
{
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void OnHandleTrigger(Transform camera, NoloDeviceType handleType)
    {
        base.OnHandleTrigger(camera, handleType);
    }
    public override void OnHoldingTrigger(Ray newRay)
    {
        base.OnHoldingTrigger(newRay);
    }
    public override void OnReleseTrigger(NoloDeviceType handleType)
    {
        base.OnReleseTrigger(handleType);
    }
    public override void OnRefresh()
    {
        base.OnRefresh();
    }
    protected override void OnDestroyThis()
    {
        base.OnDestroyThis();
    }
}
