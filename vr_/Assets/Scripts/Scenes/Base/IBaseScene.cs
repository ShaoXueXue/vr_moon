using LitJson;
using Public;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(EnterSceneEffect))]
[RequireComponent(typeof(ViewNodeManager))]
public class IBaseScene : MonoBehaviour
{
    [Header("实验名称")]
    [SerializeField]
    private string mExperienceName = "";
    public string experienceName
    {
        get
        {
            return mExperienceName;
        }
    }
    [SerializeField]
    protected PostProcessProfile m_PostProcessProfile = null;
    [SerializeField]
    private float m_OutlineThickness = 3;
    private EnterSceneEffect enterSceneEffect;

    protected List<string> capacity;
    public List<string> mCapacity
    {
        get
        {
            return capacity;
        }
        set
        {
            capacity = value;
        }
    }
    private void Awake()
    {
        //000

        OnAwake();
        

    }
    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        OnUpdate();
    }
    protected virtual void OnAwake()
    {
        capacity = new List<string> { "2", "2", "2", "2", "2" };
        ExperimentDateManager.singleton.OnRefreshExperimentEvent += OnRefresh;
    }
    private void OnDestroy()
    {
        //OnDestaryThisLab();

    }
    protected virtual void OnStart()
    {
        if (enterSceneEffect != null)
            enterSceneEffect.OnPlayAnimator();
    }
    protected virtual void OnUpdate() 
    { 
    }
    public virtual void OnStartSceneEnter()
    {
        //滤镜效果
        if (m_PostProcessProfile != null && VisualEffect.PostProcess_Apply.Instance != null)
        {
            VisualEffect.PostProcess_Apply.Instance.SetPostProcessProfile(m_PostProcessProfile);
        }
        enterSceneEffect = GetComponent<EnterSceneEffect>();
        //将实验的实例化位置写到动画里
        if (enterSceneEffect != null)
            enterSceneEffect.OnSceneEnterInit();
        //设置OutLine粗细
        SharedObjControler.Instance.OutlineEffect.lineThickness = m_OutlineThickness;
        if (ConnectionManager.Instance != null)
            ConnectionManager.Instance.ReceiveFromAndroid += OnGetDataByAndroid;
    }
    public virtual void OnDestaryThisLab()
    {
        if (ConnectionManager.Instance != null)
            ConnectionManager.Instance.ReceiveFromAndroid -= OnGetDataByAndroid;
    }
    public virtual void OnRefresh()
    {
        if(ViewNodeManager.singleton != null)
        {
            for (int i = 0; i < ViewNodeManager.singleton.viewNodeArray.Length; i++)
            {
                if (ViewNodeManager.singleton.viewNodeArray[i] == null)
                    continue;
                ViewNodeManager.singleton.viewNodeArray[i].OnRefresh();
            }
        }
        for (int i = 0; i < capacity.Count; i++)
        {
            if(capacity[i] != null)
            {
                capacity[i] = "2";
            }
        }
    }
    #region .接收安卓数据
    /// <summary>
    /// 获取安卓端数据
    /// </summary>
    /// <param name="dataByte">数据</param>
    private void OnGetDataByAndroid(string dataByte)
    {
        Experience script = JsonMapper.ToObject<Experience>(dataByte);
        if (script == null)
            return;
        //保存按钮
        if (script.buttonSaveLab.Equals("buttonSaveLab"))
        {

            int value = OnCalculateLabSource();
            //DataTools.SendDataToAndroid(value);
            List<string> var = OnSmallStepsOfExperiment();
            OnSendCapacityToAndroid(value,var);
            TestCapacity();
        }
        else   //其他按钮
        {
            if (script.buttonName == null || script.buttonName.Count <= 0)
                return;
            int index = script.buttonName[0];
            OnGetButtonIDByAndroid(index);
        }
    }
    /// <summary>
    /// 计算分数
    /// </summary>
    /// <returns></returns>
    protected virtual int OnCalculateLabSource()
    {
        return 0;
    }
    /// <summary>
    /// 小步骤完成状态
    /// </summary>
    /// <returns></returns>
    protected virtual List<string> OnSmallStepsOfExperiment()
    {
        return null;
    }
    /// <summary>
    /// 获取安卓端传来的按钮信息
    /// </summary>
    /// <param name="index">按钮信息为按钮下标</param>
    protected virtual void OnGetButtonIDByAndroid(int index)
    {

    }
    protected void OnSendCapacityToAndroid(int value,List<string> var)
    {
        if (capacity == null)
        {
            capacity = new List<string>() { "2", "2", "2", "2", "2" };
        }
        DataTools.SendCapacityToAndroid(value, capacity, var);
    }
    #endregion

    public void TestCapacity()
    {
        // 0观察能力 1思维能力 2归纳能力 3空间能力 4实验比较能力
        if (capacity[0].Equals("1"))
        {
            Debug.Log("观察能力以获得");
        }
        else if (capacity[1].Equals("1"))
        {
            Debug.Log("逻辑推理能力以获得");
        }
        else if (capacity[2].Equals("1"))
        {
            Debug.Log("归纳能力以获得");
        }
        else if (capacity[3].Equals("1"))
        {
            Debug.Log("空间能力以获得");
        }
        else if (capacity[4].Equals("1"))
        {
            Debug.Log("实验比较能力以获得");
        }
    }
}
