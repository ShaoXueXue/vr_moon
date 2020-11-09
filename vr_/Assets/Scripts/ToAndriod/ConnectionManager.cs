using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Public;
/// <summary>
/// 与安卓交互
/// 是个单例
/// </summary>
public class ConnectionManager : MonoBehaviour
{
    //单例模式
    public static ConnectionManager Instance;
    //public Text textReceive;
    //public Text sendText;
    //public Text showText;
    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {

    }
    private void Update()
    {

    }

    //委托代理
    public delegate void ReceiveData(string value);
    //需要安卓端传来数据的地方，从这里+=
    public ReceiveData ReceiveFromAndroid;

    public event DelegateT<string> OnGetUserIDEvent;

    public event DelegateT<string> OnReceiveDataByAndroidEvent;
#if NOLO_3DOF
    //创建安卓java对象
    private AndroidJavaObject jo = null;

    private AndroidJavaObject JO
    {
        get
        {
            if (jo == null)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

            }
            if (jo == null)
            {
                Debug.LogError("jo get failed!");
            }
            else
            {
                Debug.Log("jo get success!");
            }
            return jo;
        }
    }
#endif
    //向安卓发送选中仪器
    public void SendImageToAndroid(Experience exp)
    {
#if NOLO_3DOF
        string json = JsonMapper.ToJson(exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是showExperienceImages这个方法
        JO.Call("showExperienceImgs", json);
#endif

#endif
    }


    public void SendImageToAndroid(List<int> entityID)
    {
#if NOLO_3DOF
        if (entityID.Count !=1)
            return;

        Experience exp = new Experience();
        exp.imgName = entityID;
        string json = JsonMapper.ToJson(exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是showExperienceImages这个方法
        JO.Call("showExperienceImgs", json);
#endif
#endif
    }
    //发送图片名称方法重载，不走DataTools,直接发送
    public void SendDataToAndroid(Experience exp)
    {
#if NOLO_3DOF
        string json = JsonMapper.ToJson(exp);

#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("getSaveInfo", json);
#else
        Debug.LogError("向安卓端发送了成绩："+json);
#endif
#endif
    }

    //发送成绩方法重载，不走DataTools,直接发送
    public void SendDataToAndroid(int score)
    {
#if NOLO_3DOF
        Experience exp = new Experience();
        exp.score = score;
        string json = JsonMapper.ToJson(exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("getSaveInfo", json);
#endif
#endif
    }
    /// <summary>
    /// 先安卓端发送加载回调事件
    /// </summary>
    /// <param name="score"></param>
    public void SendCallBackToAndroid(Experience exp)
    {
#if NOLO_3DOF
        //Experience exp = new Experience();
        //exp.endanimation = method;
        string json = JsonMapper.ToJson(exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("endAnimation", json);
#endif
#endif
    }

    /// <summary>
    /// 向安卓端发送回调事件（小屏按钮是否可以点击）
    /// </summary>
    /// <param name="score"></param>
    public void SendButtonStateToAndroid(Experience exp)
    {
#if NOLO_3DOF
        //Experience exp = new Experience();
        //exp.endanimation = method;
        string json = JsonMapper.ToJson(exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("experimentEvent", json);
#endif
#endif
    }

    private int num = 0;
    //从安卓端接受数据
    public void FromAndroid(string value)
    {
        num++;
        //委托代理，将接收到的数据传出去
        //showText.text = value+"           \n"+num.ToString();
        Experience exp = JsonMapper.ToObject<Experience>(value);
        ReceiveFromAndroid?.Invoke(value);
    }
    /// <summary>
    /// 从安卓端接受数据
    /// </summary>
    /// <param name="value"></param>
    public void OnGetExperimentDateByAndroid(string value)
    {
        Debug.Log("接收到的数据：" + value);
        //ExperienceNewSteps exp = JsonMapper.ToObject<ExperienceNewSteps>(value);
        //if(exp == null)
        //{
        //    Debug.Log("步骤信息解析失败");
        //}
        if (OnReceiveDataByAndroidEvent != null)
            OnReceiveDataByAndroidEvent(value);
    }
    /// <summary>
    /// 向安卓端发送数据(已强转类型)
    /// </summary>
    public void OnShowExperienceSteps(string exp)
    {
#if NOLO_3DOF
        //string json = JsonMapper.ToJson(exp);
        //Debug.Log("向安卓发送步骤信息："+ exp);
#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("OnShowExperienceSteps", exp);
        Debug.Log("OnShowExperienceSteps向安卓发送步骤信息："+ exp);
#else
        Debug.LogError("向安卓端发送了步骤信息：" + exp);
#endif
#endif
    }

    /// <summary>
    /// 应用被打开，向安卓发送消息，告诉安卓需要发送唯一ID了。
    /// </summary>
    public void OnStartSceneToAndroid()
    {
#if !UNITY_EDITOR
        //安卓端接受消息的是getSaveInfo这个方法
        JO.Call("OnStartSceneToAndroid");
        Debug.Log("OnStartSceneToAndroid向安卓发送程序启动信息");
#else
        Debug.LogError("向安卓端发送了开始消息：");
#endif
    }
    public void OnGetUserIdByAndroid(string index)
    {
        if (OnGetUserIDEvent != null)
            OnGetUserIDEvent(index);
    }

    /// <summary>
    /// 通知安卓加载ab进度
    /// </summary>
    /// <param name="_value">true: 开始加载，false: 加载完成</param>
    public void OnLoadABSingleToAndroid(string _value)
    {
#if !UNITY_EDITOR
         JO.Call("OnLoadAB", _value);
        Debug.Log("OnLoadABSingleToAndroid向安卓发送资源加载信号："+ _value);
#else
        Debug.LogError("向安卓发送资源加载信号：" + _value);
#endif
       
    }
}
