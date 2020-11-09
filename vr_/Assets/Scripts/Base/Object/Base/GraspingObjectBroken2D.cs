using Public;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 具有破碎效果的物体拾取基类不具有拉近功能
/// </summary>
public class GraspingObjectBroken2D : GraspingObjectBase2D
{
    public Transform brokenTra;
    public bool BrokStyle
    {
        get;
        set;
    }
    protected bool _isBroken;
    #region .破碎
    [Tooltip("是否可以摔碎")]
    [SerializeField]
    private bool isBroken = false;
    [Tooltip("完整的烧杯")]
    [SerializeField]
    private GameObject fullCap = null;
    [Tooltip("破碎物体管理类")]
    [SerializeField]
    private Lab_BrokenEffManager brokenEffManager = null;
    [Tooltip("破碎音效")]
    [SerializeField]
    private AudioClip m_BreakAudio = null;
    private BrokenEff currentbrokenEff = null;
    public float m_BreakSpeedSqr = 2f;
    public float m_BreakMass = 0.9f;

    public event DelegateT<bool> OnBrokenEvent;                     
    #endregion
    public override void OnInit()
    {
        base.OnInit();
        BrokStyle = false;
        brokenEffManager.OnRestoreEvent += BrokenEff_OnRestoreEvent;
        brokenEffManager.OnInit();
    }
    public override void OnRefresh()
    {
        base.OnRefresh();
        if (brokenEffManager != null)
            brokenEffManager.OnRefresh();
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
        if (brokenTra == null)
            return;
        if (transform.position.y >= brokenTra.position.y)
            _isBroken = true;
        else
            _isBroken = false;
    }
    protected override void OnDestroyThis()
    {
        base.OnDestroyThis();
        //brokenEffManager.OnRestoreEvent -= BrokenEff_OnRestoreEvent;
    }
    public override void OnRefreshHandle()
    {
        base.OnRefreshHandle();
    }
    #region .破碎修复
    protected void BrokenEff_OnRestoreEvent()
    {
        OnRestore();
    }
    protected void OnRestore()
    {
        mRigidbody2D.simulated = true;
        transform.parent = defaultParent;
        transform.localPosition = defaultLoacalPos;
        transform.eulerAngles = defaultWorldRot;
        BrokStyle = false;
        fullCap.SetActive(true);
        if (OnBrokenEvent != null)
            OnBrokenEvent(true);
        if (currentbrokenEff != null)
            currentbrokenEff.OnRestore();
    }
    protected void OnRestoreStep()
    {
        fullCap.SetActive(true);
        if (currentbrokenEff != null)
            currentbrokenEff.OnRestore();
    }
    #endregion
    #region .产生破碎
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isBroken)
            return;
        if (!isBroken)
            return;
        if (!OnGetState(collision))
            return;
        OnBroken();
    }
    /// <summary>
    /// 破碎时调用的虚方法
    /// </summary>
    protected virtual void OnBrokenLater()
    {

    }
    public  void OnBroken()
    {
        mRigidbody2D.simulated = false;
        fullCap.SetActive(false);
        BrokStyle = true;
        OnInputInformation();
        OnBrokenLater();
        if (currentbrokenEff != null)
            currentbrokenEff.OnBroken();
        if (OnBrokenEvent != null)
            OnBrokenEvent(false);
        if (m_BreakAudio != null && AudioManager.Instance != null)
            AudioManager.Instance.Play_Start(m_BreakAudio);
    }

    private bool OnGetState(Collision2D other)
    {
        if (other.rigidbody.mass < m_BreakMass)
            return false;
        if (other.relativeVelocity.sqrMagnitude < m_BreakSpeedSqr)
            return false;
        return true;
    }

    //获取当前需要破碎物体的状态 
    private void OnInputInformation()
    {
        OnSetBrokenEff();
    }

    private void OnSetBrokenEff()
    {
        if (brokenEffManager == null)
            return;
        currentbrokenEff = brokenEffManager.OnGetBrokenEff();
    }
    #endregion
}
