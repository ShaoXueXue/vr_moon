using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 破碎物体
/// </summary>
public class GraspingObjectBroken3D : GraspingObjectBase3D
{
    #region .破碎
    [Tooltip("是否可以摔碎")]
    [SerializeField]
    private bool isBroken = false;
    [Tooltip("完整的烧杯")]
    [SerializeField]
    private GameObject fullCap=null;
    [Tooltip("破碎物体管理类")]
    [SerializeField]
    private Lab_BrokenEffManager brokenEffManager=null;
    [Tooltip("破碎音效")]
    [SerializeField]
    private AudioClip m_BreakAudio = null;
    private BrokenEff currentbrokenEff=null;
    public float m_BreakSpeedSqr = 2f;
    public float m_BreakMass = 0.9f;
    #endregion
    public override void OnInit()
    {
        base.OnInit();
        brokenEffManager.OnRestoreEvent += BrokenEff_OnRestoreEvent;
        brokenEffManager.OnInit();
    }
    public override void OnRefresh()
    {
        base.OnRefresh();
        if (brokenEffManager != null)
            brokenEffManager.OnRefresh();
    }
    protected override void OnDestroyThis()
    {
        brokenEffManager.OnRestoreEvent -= BrokenEff_OnRestoreEvent;
    }
    #region .破碎修复
    private void BrokenEff_OnRestoreEvent()
    {
        OnRestore();
    }

    private void OnRestore()
    {
        m_rigidbody.isKinematic = true;
        transform.parent = defaultParent;
        transform.localPosition = defaultLoacalPos;
        transform.eulerAngles = defaultWorldRot;
        fullCap.SetActive(true);
        if (currentbrokenEff != null)
            currentbrokenEff.OnRestore();
    }
    #endregion
    #region .产生破碎
    private void OnCollisionEnter(Collision collision)
    {
        if (!OnGetState(collision))
            return;
        OnBroken();
    }

    private void OnBroken()
    {
        m_rigidbody.isKinematic = false;
        fullCap.SetActive(false);
        OnInputInformation();
        if (currentbrokenEff != null)
            currentbrokenEff.OnBroken();
        if (m_BreakAudio != null && AudioManager.Instance != null)
            AudioManager.Instance.Play_Start(m_BreakAudio);
    }

    private bool OnGetState(Collision other)
    {
        if (!other.transform.GetComponent<Rigidbody>())
            return false;
        if (other.rigidbody.mass < m_BreakMass)
            return false;
        if (other.relativeVelocity.sqrMagnitude < m_BreakSpeedSqr)
            return false;
        if (this.gameObject.name.Equals("Beaker_Half")&& other.gameObject.name.Equals("Rig_Funnel"))
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
