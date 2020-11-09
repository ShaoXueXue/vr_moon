using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CamFade : MonoBehaviour
{
    [SerializeField] private Image m_FadeImag;
    public static CamFade Instance { private set; get; }
    private Color TransColor = new Color(0, 0, 0, 0);
    private Color BlackColor = new Color(0, 0, 0, 1);
    private void Awake()
    {
        Initial();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    private void Initial()
    {
        Instance = this;
        if (m_FadeImag == null) m_FadeImag = GetComponentInChildren<Image>();

    }

    public void StartFade(float duration, bool toBlack)
    {
       // Debug.Log($"StartFade toBlack:  {toBlack} duration:{duration} now:{Time.frameCount}");

        //if ((toBlack && m_FadeImag.color.a < 0.9f)||
        //    (!toBlack && m_FadeImag.color.a > 0.1f))
        //{
        //    return;
        //}
        
        StopAllCoroutines();

        if (duration == 0)
        {
            m_FadeImag.color = toBlack ? BlackColor : TransColor;
            return;
        }

        StartCoroutine(Fade(duration, toBlack));
    }

    public void StartHardFade(float duration, bool toBlack)
    {
        // Debug.Log($"StartFade toBlack:  {toBlack} duration:{duration} now:{Time.frameCount}");

        //if ((toBlack && m_FadeImag.color.a < 0.9f)||
        //    (!toBlack && m_FadeImag.color.a > 0.1f))
        //{
        //    return;
        //}
        //StopAllCoroutines();

        if (duration == 0)
        {
            m_FadeImag.color = toBlack ? BlackColor : TransColor;
            return;
        }
        StartCoroutine(HardFade(duration, toBlack));
    }


    private IEnumerator Fade(float duration, bool toBlack)
    {
        float timer = duration;
        Color fromColor = toBlack ? TransColor : BlackColor;
        Color toColor = toBlack ? BlackColor : TransColor ;

        m_FadeImag.color = toBlack ? TransColor : BlackColor;
        //m_FadeImag.enabled = true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float proportion = Mathf.InverseLerp(duration, 0, timer);
            m_FadeImag.color = Color.Lerp(fromColor, toColor, proportion);
            yield return null;
        }
        m_FadeImag.color = toColor;
    }


    public IEnumerator HardFade(float duration, bool toBlack)
    {
        float timer = duration;
        Color fromColor = toBlack ? TransColor : BlackColor;
        Color toColor = toBlack ? BlackColor : TransColor;

        m_FadeImag.color = toBlack ? TransColor : BlackColor;
        //m_FadeImag.enabled = true;
        yield return new WaitForSeconds(0.5f);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float proportion = Mathf.InverseLerp(duration, 0, timer);
            m_FadeImag.color = Color.Lerp(fromColor, toColor, proportion);
            yield return null;
        }
        m_FadeImag.color = toColor;
    }


}
