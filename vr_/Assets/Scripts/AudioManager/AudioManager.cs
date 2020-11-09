using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource[] m_AudioSources;
    private int m_SourceLength;
    private int m_CurrentSource;
    private void Start()
    {
        Initial();

    }
    private void Initial()
    {
        Instance = this;
        m_AudioSources = GetComponents<AudioSource>();
        m_SourceLength = m_AudioSources.Length;
        m_CurrentSource = 0;
    }

    public void Play_Delay(AudioClip audioClip,float wait)
    {
        StartCoroutine(Delay(audioClip,wait));
    }

    private IEnumerator Delay(AudioClip audioClip, float wait)
    {
        yield return new WaitForSeconds(wait);
        Play_Start(audioClip);
    }
    public int Play_Start(AudioClip audioClip, bool isLoop = false, float startVolume = 1)
    {
        //Debug.Log($"------------Play_Start: {audioClip.name}");
        if (audioClip == null) return 0;

        m_CurrentSource = (m_CurrentSource + 1) % m_SourceLength;

        if (m_AudioSources[m_CurrentSource].isPlaying)
            m_AudioSources[m_CurrentSource].Stop();

        m_AudioSources[m_CurrentSource].clip = audioClip;
        m_AudioSources[m_CurrentSource].loop = isLoop;
        m_AudioSources[m_CurrentSource].volume = startVolume;
        m_AudioSources[m_CurrentSource].Play();

        //Debug.Log($"Audio Clip: { audioClip.name} on index:{m_CurrentSource} ");

        return m_CurrentSource;
    }
    public void Playing_Volume(AudioClip audioClip, int sourceIndex, float volume)
    {
        //Debug.Log($"Playing_Volume: {audioClip.name} volume: {volume}");
        if (m_AudioSources[sourceIndex].clip != audioClip) return;

        if (!m_AudioSources[sourceIndex].isPlaying) return;

        m_AudioSources[sourceIndex].volume = volume;
    }

    public void Play_Stop(AudioClip audioClip, int sourceIndex)
    {
        if (m_AudioSources[sourceIndex].clip != audioClip) return;

        if (m_AudioSources[sourceIndex].isPlaying)
            m_AudioSources[sourceIndex].Stop();

        m_AudioSources[m_CurrentSource].clip = null;
    }

    //新增声音管理初始化。当突然且场景之类的得操作发生而正在播放声音时，要将声音关掉
    public void OnRefresh()
    {
        if (m_AudioSources[m_CurrentSource].isPlaying)
            m_AudioSources[m_CurrentSource].Stop();
    }

}
