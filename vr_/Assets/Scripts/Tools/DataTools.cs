using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具类
/// </summary>
public static class DataTools
{
    /// <summary>
    ///  向安卓端发送图片名称（实验器材的entityID）
    /// </summary>
    /// <param name="entityID">器材对应的PPT中的下标</param>
    public static void SendImageToAndroid(List<int> entityID)
    {
        Experience exp = new Experience();
        exp.imgName = entityID;
        ConnectionManager.Instance.SendImageToAndroid(exp);
    }
    /// <summary>
    /// 向安卓端发送实验得分
    /// </summary>
    /// <param name="score">该实验所得分数</param>
    public static void SendDataToAndroid(int score)
    {
        Experience exp = new Experience();
        exp.score = score;
        ConnectionManager.Instance.SendDataToAndroid(exp);
    }
    /// <summary>
    /// 向安卓端发送能力得分/保存实验发送数据
    /// </summary>
    /// <param name="score">该实验所得分数</param>
    public static void SendCapacityToAndroid(int value,List<string> score,List<string> smallStep)
    {
        Experience exp = new Experience();
        exp.capacity = score;
        for (int i = 0; i < exp.capacity.Count; i++)
        {
            Debug.Log("能力下标：" + i + "==是否获得该能力: " + exp.capacity[i]);
        }
        exp.score = value;
        exp.experimentSmallSteps = smallStep;
        ConnectionManager.Instance.SendDataToAndroid(exp);
        //ConnectionManager.Instance.SendDataToAndroid(exp);
    }
    /// <summary>
    /// 向安卓端发送加载回调
    /// </summary>
    /// <param name="method"></param>
    public static void SendCallBackToAndroid(string method)
    {
        Experience exp = new Experience();
        exp.endanimation = method;
        ConnectionManager.Instance.SendCallBackToAndroid(exp);
    }

    /// <summary>
    /// 向安卓端发送加载回调
    /// </summary>
    /// <param name="method"></param>
    public static void SendButtonStateToAndroid(string buttonState)
    {
        Experience exp = new Experience();
        exp.experimentEvent = buttonState;
        ConnectionManager.Instance.SendButtonStateToAndroid(exp);
    }
    /// <summary>
    /// 向安卓端发送步骤信息
    /// </summary>
    /// <param name="var"></param>
    public static void OnSendExperienceStepData(string var)
    {
        ConnectionManager.Instance.OnShowExperienceSteps(var);
    }
    /// <summary>
    /// 应用被打开，向安卓发送消息，告诉安卓需要发送唯一ID了。
    /// </summary>
    public static void OnStartSceneToAndroid()
    {
        ConnectionManager.Instance.OnStartSceneToAndroid();
    }
}

