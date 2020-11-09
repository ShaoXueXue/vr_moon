using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D碰撞拾取
/// </summary>
public class GraspingObjectBase3D : GraspingObjectBase
{
    //刚体
    [SerializeField]
    protected Rigidbody m_rigidbody;
    //碰撞框
    [SerializeField]
    protected Collider m_collider;
    [SerializeField]
    protected Vector3 defaultWorldPos;

    //public override void OnPickUp()
    //{
    //    if (m_collider != null)
    //        m_collider.isTrigger = true;
    //    if (m_rigidbody != null)
    //        m_rigidbody.isKinematic = true;
    //}
    //public override void OnPutDown()
    //{
    //    if (m_collider != null)
    //        m_collider.isTrigger = false;
    //    if (m_rigidbody != null)
    //        m_rigidbody.isKinematic = false;
    //}


    public override void OnInit()
    {
        base.OnInit();
        defaultWorldPos = transform.position;
        if (m_rigidbody == null)
            m_rigidbody = GetComponent<Rigidbody>();
        if (m_collider == null)
            m_collider = GetComponent<Collider>();
    }
    //手柄指向某东西
    public override void OnRayEnter()
    {
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].enabled = true;
            outLine[i].color = 0;
        }
    }
    public override void OnRayExit()
    {
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].color = 0;
            outLine[i].enabled = false;
        }
    }

    /// <summary>
    /// 拾取
    /// </summary>
    /// <param name="camera"></param>
    public override void OnHandleTrigger(Transform camera, NoloDeviceType handleType)
    {
        mHandleType = handleType;
        transform.eulerAngles = targetRot;
        isPut = false;
        isHandle = true;
        cameraTra = camera;
        m_IntersectPlane = (cameraTra.position.z < 0) ? m_IntersectPlanePositive : m_IntersectPlaneNegative;
        OnPickUp();
    }
    /// <summary>
    /// 拾取中
    /// </summary>
    /// <param name="newRay"></param>
    public override void OnHoldingTrigger(Ray newRay)
    {
       // Debug.LogError(DeviceTrans);
        if (DeviceTrans == null) return;
        Ray deviceRay = newRay;
        Vector3 hitPos;
        if (RayTriInsecPos(m_IntersectPlane[0], m_IntersectPlane[1], m_IntersectPlane[2], deviceRay, out hitPos))
        {
            for (int i = 0; i < outLine.Length; i++)
            {
                if (outLine[i] == null)
                    continue;
                outLine[i].enabled = true;
                outLine[i].color = 1;
            }
            RaycastHit interactHit;
            if (Physics.Raycast(DeviceTrans.position, DeviceTrans.forward, out interactHit, 1000))
            {
                float hitZ = interactHit.point.z;
               // Debug.LogError(hitZ);
                float offsetZ = (hitZ > 0) ? hitZ + m_PickOffset_z : m_PickOffset_z;
                transform.position = hitPos + Vector3.forward * offsetZ;
            }
            else
                transform.position = hitPos + Vector3.forward * m_PickOffset_z;
        }
    }


    /// <summary>
    /// 放下
    /// </summary>
    public override void OnReleseTrigger(NoloDeviceType handleType)
    {
        isPut = true;
        mHandleType = handleType;
        SetParent();
        OnPutDown();
        transform.eulerAngles = defaultWorldRot;
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].color = 0;
            outLine[i].enabled = false;
        }
    }
    /// <summary>
    /// 被手捡起
    /// </summary>
    public override void OnPickUp()
    {

    }
    /// <summary>
    /// 被手放下
    /// </summary>
    public override void OnPutDown()
    {
    }
    /// <summary>
    /// 将物体放入物体上面
    /// </summary>
    public override void OnPutItOnTheObject()
    {

    }
    private bool isPutItOnTheObject;
    public bool IsPutItOnTheObject
    {
        get
        {
            return isPutItOnTheObject;
        }
    }
    public void OnSetPutItOnTheObject(bool state)
    {
        isPutItOnTheObject = state;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnRefresh()
    {
        SetParent();
        this.transform.localPosition = defaultLoacalPos;
        this.transform.eulerAngles = defaultWorldRot;
        OnPutDown();
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].enabled = false;
            outLine[i].color = 0;
        }
        isPutItOnTheObject = false;
    }
    public override void OnRefreshHandle()
    {
        base.OnRefreshHandle();
    }
    /// <summary>
    /// 清扫初始化
    /// </summary>
    public override void OnSweep()
    {

    }
    protected override void OnDestroyThis()
    {
        base.OnDestroyThis();
    }

}
