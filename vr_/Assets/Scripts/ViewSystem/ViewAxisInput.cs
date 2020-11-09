using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ViewAxisInput
{
    #region Hold Singleton mode
    private static ViewAxisInput m_Instance;
    public static ViewAxisInput Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new ViewAxisInput();
            }
            return m_Instance;
        }
    }
    private ViewAxisInput()
    {
        CinemachineCore.GetInputAxis = GetTouchAxis;
    
    }
    #endregion

    private bool m_AxisInputing = false;
    private Vector2 m_InputAxis = Vector2.zero;

    public float GetTouchAxis(string axisName)
    {

        if (axisName == "Axis_X")
        {
            return m_InputAxis.x;
        }
        else if (axisName == "Axis_Y")
        {
            return m_InputAxis.y;
        }

        return 0;
    }

    public void StartAxis_Input()
    {
        m_AxisInputing = true;
    }
    public void ProcessAxis_Input(Vector2 deltaInput)
    {
        //Debug.Log("旋转轴:" + deltaInput + "进入这里代表旋转可以使用 + 3");
        m_InputAxis = (deltaInput / Time.deltaTime);
      //  Debug.Log(m_InputAxis.x);
    }
    public void EndAxis_Input()
    {
        m_AxisInputing = false;
        m_InputAxis = new Vector2(0, 0);
      
    }
}
