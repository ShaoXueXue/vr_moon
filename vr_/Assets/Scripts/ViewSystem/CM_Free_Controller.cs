using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Public;
[RequireComponent(typeof(CinemachineFreeLook))]
public class CM_Free_Controller : MonoBehaviour
{
    private CinemachineFreeLook m_CM_Free=null;
    private const float m_RangeX = 25;
    private const float m_SmallAngle = 5;
   // private bool m_OnOtherSide = false;
    private bool m_Fading = false;
    public static event DelegateT BackHandle=null;
    private void Start()
    {
        m_CM_Free = GetComponent<CinemachineFreeLook>();

    }
    private void OnEnable()
    {
        BackHandle += BackToPositive;
    }
    private void OnDisable()
    {
        BackHandle -= BackToPositive;
        //Level_OneStatus.Instance.SetHandsDisplay(true);
        StopAllCoroutines();
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_Fading) return;

        if (m_CM_Free.m_XAxis.Value < 90 && m_CM_Free.m_XAxis.Value > m_RangeX)
        {
            StartBlendingTo(180 - m_RangeX + m_SmallAngle);
        }
        else if (m_CM_Free.m_XAxis.Value > 90 && m_CM_Free.m_XAxis.Value < 180 - m_RangeX)
        {
            StartBlendingTo(m_RangeX - m_SmallAngle);
        }
        else if (m_CM_Free.m_XAxis.Value > -90 && m_CM_Free.m_XAxis.Value < -m_RangeX)
        {
            StartBlendingTo(-180 + m_RangeX - m_SmallAngle);
        }
        else if (m_CM_Free.m_XAxis.Value < -90 && m_CM_Free.m_XAxis.Value > -180 + m_RangeX)
        {
            StartBlendingTo(-m_RangeX + m_SmallAngle);
        }
    }

    //TODO reset CM follow look at
    public static void AllBackToPositive()
    {
        BackHandle?.Invoke();
    }
    private void BackToPositive()
    {
        if (m_CM_Free.m_XAxis.Value < -90 || m_CM_Free.m_XAxis.Value > 90) StartBlendingTo(0);
    }
    private void StartBlendingTo(float targetAngle)
    {
        Debug.Log($"StartBlendingTo {targetAngle}");
        StopAllCoroutines();
        StartCoroutine(BlendingTo(targetAngle));
    }
    private const float m_AngleSpeed = 15f;
    private IEnumerator BlendingTo(float targetAngle)
    {
        m_Fading = true;
        //Level_OneStatus.Instance.SetHandsDisplay(false);
        float angleSpeed = m_AngleSpeed;
        if (targetAngle < m_CM_Free.m_XAxis.Value) angleSpeed *= -1;
        ViewAxisInput.Instance.StartAxis_Input();
        while (true)
        {
            if ((angleSpeed < 0 && m_CM_Free.m_XAxis.Value < targetAngle) ||
                (angleSpeed > 0 && m_CM_Free.m_XAxis.Value > targetAngle)) break;

            ViewAxisInput.Instance.ProcessAxis_Input(new Vector2(angleSpeed * Time.deltaTime, 0));
            yield return null;
        }
        ViewAxisInput.Instance.EndAxis_Input();
        m_Fading = false;
        //Level_OneStatus.Instance.SetHandsDisplay(true);
    }
}
