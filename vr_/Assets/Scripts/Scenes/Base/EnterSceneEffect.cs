using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
/// <summary>
/// 入场动画
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class EnterSceneEffect : MonoBehaviour
{
    //后期需要添加到Editor中，通过类型去在窗口展示不同的属性（动画切换的属性，时间，坐标）
    public enum EnterSceneAnimatorType
    {
        ByDefault,    //默认的不包含动画
        ByCiemachineVirtual,
        ByCinemachineFreeLook,
    }
    public EnterSceneAnimatorType animationType;
    [Tooltip("入场音效")]
    public AudioClip mClip = null;
    private AudioSource mSource;
    public void OnSceneEnterInit()
    {
        transform.position = Vector3.zero;
        mSource = GetComponent<AudioSource>();
        mSource.playOnAwake = true;
    }
    public void OnPlayAnimator()
    {
        switch (animationType)
        {
            case EnterSceneAnimatorType.ByDefault:
                Invoke("AnimtionByDefault", defaultWaitTime);
                break;
            case EnterSceneAnimatorType.ByCiemachineVirtual:
                Invoke("AnimtionByDoTween", CiemachineVirtualCameraWaitTime);
                break;
            case EnterSceneAnimatorType.ByCinemachineFreeLook:
                Invoke("AnimtionByCinemachineFreeLook", waitTime);
                break;
            default:
                break;
        }
    }
    //ByCinemachineFreeLook
    [Tooltip("CinemachineFreeLook的Transform")]
    public Transform[] animationCameras;
    [Tooltip("当前CinemachineFreeLook的m_YAxis的值")]
    public float mYAxisValue;
    [Tooltip("调用此动画的等待时间")]
    public float waitTime;

    private void AnimtionByCinemachineFreeLook()
    {
        OnPlayAudio();
        if (animationCameras == null || animationCameras.Length <= 0)
            return;
        animationCameras[0].GetChild(0).GetComponent<CinemachineVirtualCameraBase>().enabled = false;
        animationCameras[0].gameObject.SetActive(false);
        //打开
        animationCameras[1].gameObject.SetActive(true);
        animationCameras[1].GetChild(0).GetComponent<CinemachineFreeLook>().m_YAxis.Value = mYAxisValue;
        animationCameras[1].GetChild(0).GetComponent<CinemachineFreeLook>().enabled = true;
    }
    //ByCiemachineVirtual
    [SerializeField]
    [Tooltip("CiemachineVirtualCamera的Transform")]
    public Transform[] CiemachineVirtualCamera;
    [Tooltip("调用此动画的等待时间")]
    public float CiemachineVirtualCameraWaitTime;
    private void AnimtionByDoTween()
    {
        OnPlayAudio();
        if (CiemachineVirtualCamera.Length!=0)
        {
            CiemachineVirtualCamera[0].gameObject.SetActive(false);
            //打开
            CiemachineVirtualCamera[1].gameObject.SetActive(true);
        }
       
    }


    [Tooltip("调用此动画的等待时间")]
    public float defaultWaitTime;
    public void AnimtionByDefault() 
    {
        OnPlayAudio();
    }


    private void OnPlayAudio()
    {
        if(mSource != null && mClip != null)
        {
          //  Debug.Log("play audio");
            mSource.loop = false;
            mSource.clip = mClip;
            mSource.Play();
        }
    }
}
