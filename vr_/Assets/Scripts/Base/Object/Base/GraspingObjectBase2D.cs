using NoloVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2D物理碰撞拾取
/// </summary>
public class GraspingObjectBase2D : GraspingObjectBase
{
    protected Collider2D[] mCollider2D;
    public Collider2D[] _mCollider2D
    {
        get
        {
            return mCollider2D;
        }
    }
    [SerializeField]
    protected Rigidbody2D mRigidbody2D;
    private RigidbodyConstraints2D m_OriginalConstraints2D;

    protected Vector3 handlePos;
    public override void OnInit()
    {
     
        base.OnInit();
        handlePos = defaultLoacalPos;
        mCollider2D = GetComponents<Collider2D>();

        if(mRigidbody2D != null)
            m_OriginalConstraints2D = mRigidbody2D.constraints;
        isPutItOnTheObject = false;
    }
    public override void OnHandleTrigger(Transform camera, NoloDeviceType handleType)
    {
        base.OnHandleTrigger(camera, handleType);
        OnSetPutItOnTheObject(false);
        handlePos = transform.localPosition;
        OnPickUp();
    }
    public override void OnHoldingTrigger(Ray newRay)
    {
        base.OnHoldingTrigger(newRay);
    }
    public override void OnReleseTrigger(NoloDeviceType handleType)
    {
        base.OnReleseTrigger(handleType);
    }
    public override void OnPickUp()
    {
        base.OnPickUp();
        for (int i = 0; i < mCollider2D.Length; i++)
        {
            if (mCollider2D[i] == null)
                continue;
            mCollider2D[i].isTrigger = true;
        }
        if (mRigidbody2D != null)
        {
            mRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            mRigidbody2D.simulated = true;
            mRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public override void OnPutDown()
    {
        base.OnPutDown();
        for (int i = 0; i < mCollider2D.Length; i++)
        {
            if (mCollider2D[i] == null)
                continue;
            mCollider2D[i].isTrigger = false;
        }
        if (mRigidbody2D != null)
        {
            mRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            mRigidbody2D.simulated = true;
            mRigidbody2D.constraints = m_OriginalConstraints2D;
        }
    }
    public override void OnPutItOnTheObject()
    {
        base.OnPutItOnTheObject();
        for (int i = 0; i < mCollider2D.Length; i++)
        {
            if (mCollider2D[i] == null)
                continue;

            mCollider2D[i].isTrigger = false;
        }
        if (mRigidbody2D != null)
        {
            mRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            mRigidbody2D.simulated = true;
            mRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public override void OnRefresh()
    {
        base.OnRefresh();
        handlePos = defaultLoacalPos;
        if(mRigidbody2D != null)
        {
            mRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            mRigidbody2D.constraints = m_OriginalConstraints2D;
        }
        isPutItOnTheObject = false;

    }
    /// <summary>
    /// 切换场景用来初始化物体属性的
    /// </summary>
    public override void OnRefreshHandle()
    {
        base.OnRefreshHandle();
        if (!isPut)
        {
            SetParent();
            for (int i = 0; i < outLine.Length; i++)
            {
                if (outLine[i] == null)
                    continue;
                outLine[i].color = 0;
                outLine[i].enabled = false;
            }
            transform.eulerAngles = defaultWorldRot;
            isPut = true;
            transform.localPosition = handlePos;
            if (mHandleType == NoloDeviceType.LeftController)
            {
                //Debug.Log("左手物体为放开");
                NoloHandleRay.singleton.leftHandleObj = null;
                NoloHandleRay.singleton.IsHandleLeftObj = false;
            }
            else if (mHandleType == NoloDeviceType.RightController)
            {
                //Debug.Log("右手物体为放开");
                NoloHandleRay.singleton.rightHandleObj = null;
                NoloHandleRay.singleton.IsHandleRightObj = false;
            }
            OnPutDown();
        }
    }
    public override void OnSweep()
    {
        base.OnSweep();
    }

    /// <summary>
    /// 是否放到其他物体上
    /// </summary>
    private bool isPutItOnTheObject; 
    public bool IsPutItOnTheObject
    {
        get
        {
            return isPutItOnTheObject;
        }
    }
    /// <summary>
    /// 是否放到其他物体上
    /// </summary>
    /// <param name="state"></param>
    public void OnSetPutItOnTheObject(bool state)
    {
        isPutItOnTheObject = state;
    }
    private const float m_RepulsiveForce = 0.001f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        DeathGround2D script = collision.transform.GetComponent<DeathGround2D>();
        if (script != null)
        {
            SetParent();
            transform.eulerAngles = defaultWorldRot;
            transform.localPosition = defaultLoacalPos;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DeathGround2D script = collision.GetComponent<DeathGround2D>();
        if (script != null)
        {
            SetParent();
            transform.eulerAngles = defaultWorldRot;
            transform.localPosition = defaultLoacalPos;
        }
    }
    /// <summary>
    /// 删除脚本
    /// </summary>
    protected override void OnDestroyThis()
    {
        base.OnDestroyThis();
    }
    //Vector3 _posion;
    private void FixedUpdate()
    {
        //Vector3 var = transform.localPosition;
        //if(_posion == var)
        //{
        //    //Debug.Log("怎么能进来啊：" + transform.name + "速率是多少啊：" + mRigidbody2D.velocity.magnitude);
        //    isHandle = false;
        //    _posion = var;
        //}
        //else
        //{
        //    isHandle = true;
        //    _posion = var;
        //}
    }
}
