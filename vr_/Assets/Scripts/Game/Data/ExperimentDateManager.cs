using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Public;
/// <summary>
/// 实验数据管理类（大步骤记录）
/// </summary>
public class ExperimentDateManager : MonoBehaviour
{
    public const string resetButton = "重置";  //重置


    public event DelegateT<ExperimentStep> OnGetDataByAndroidEvent;
    public event DelegateT<string,JsonData> OnGetDataByAndroidJsonEvent;
    public event DelegateT OnRefreshExperimentEvent;


    ExperienceNewSteps currentExperimentData;
    static ExperimentDateManager _singleton;
    public static ExperimentDateManager singleton
    {
        get
        {
            return _singleton;
        }
    }
    private void Start()
    {
        _singleton = this;
        OnGreatExperimentDate();
        ConnectionManager.Instance.OnReceiveDataByAndroidEvent += OnGetDataByAndroid;
    }
    /// <summary>
    /// 1.先调用创建步骤方法
    /// </summary>
    void OnGreatExperimentDate()
    {
        if (currentExperimentData == null)
        {
            currentExperimentData = new ExperienceNewSteps();
            currentExperimentData.stepData = new List<ExperimentStep>();
        }
        else
        {

            currentExperimentData.stepData.Clear();
        }
        currentExperimentData.resetButton = "";
        currentExperimentData.stepIndex = "";
    }


    /// <summary>
    /// 外部调用，添加步骤
    /// </summary>
    /// <param name="index"></param>
    /// <param name="var"></param>
    public void OnGreatExpermentStepItem(string index, object var)
    {
        ExperimentStepItem script = new ExperimentStepItem();
        if (script == null)
            return;
        script.mJson = var;
        OnAddItemStep(index,script);
    }
    /// <summary>
    /// 添加单步骤物体中的数据
    /// </summary>
    /// <param name="var"></param>
    void OnAddItemStep(string index,ExperimentStepItem var)
    {
        if (var == null)
            return;
        if (currentExperimentData == null) 
        {
            OnGreatExperimentDate();
        }
        if(currentExperimentData.stepData == null)
        {
            currentExperimentData.stepData = new List<ExperimentStep>();
        }
        ExperimentStep script = OnContains(currentExperimentData.stepData, index);
        //string itemStepData = JsonMapper.ToJson(var);
        if (script == null)
        {
            script = new ExperimentStep();
            script.stepIndex = index;
            script.stepItemArray = new List<ExperimentStepItem>();
            script.stepItemArray.Add(var);
            OnAddDataStep(script);
        }
        else
        {
            script.stepItemArray.Add(var);
        }
    }
    /// <summary>
    /// 当前步骤中是否存在此index步骤
    /// </summary>
    /// <param name="listArray"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    ExperimentStep OnContains(List<ExperimentStep> listArray,string index)
    {
        for (int i = 0; i < listArray.Count; i++)
        {
            if (listArray[i] == null)
                continue;
            if (listArray[i].stepIndex.Equals(index))
                return listArray[i];
        }
        return null;
    }
    /// <summary>
    /// 添加实验步骤
    /// </summary>
    /// <param name="var"></param>
    void OnAddDataStep(ExperimentStep var)
    {
        if (var == null)
            return;
        currentExperimentData.stepData.Add(var);
    }

    /// <summary>
    /// 向安卓端发送数据
    /// </summary>
    public void OnSendDataToAndroid()
    {
        string var = JsonMapper.ToJson(currentExperimentData);
        DataTools.OnSendExperienceStepData(var);
    }
    JsonData stepData;
    /// <summary>
    /// 接收安卓端信息
    /// </summary>
    void OnGetDataByAndroid(string var)
    {
        JsonData exp = JsonMapper.ToObject(var);
        if (exp == null)
        {
            Debug.Log("步骤信息解析失败");
        }
        if (exp["resetButton"].Equals(resetButton))
        {
            Debug.Log("点击了重置按钮："+ exp["resetButton"] + "===Unity比对数值" + resetButton);
            if (OnRefreshExperimentEvent != null)
                OnRefreshExperimentEvent();
            return;
        }
        if (exp["stepIndex"].Equals(""))
        {
            Debug.Log("点击了步骤按钮按钮：" + exp["resetButton"] + "为空，所以不进行解析");
            return;
        }
        stepData = exp["stepData"];
        JsonData script = OnContains((string)exp["stepIndex"], exp["stepData"]);
        if(script == null)
        {
            Debug.Log("未从android端根据index在步骤中获取对应的itemStep");
            return;
        }
        if (OnGetDataByAndroidJsonEvent != null)
            OnGetDataByAndroidJsonEvent((string)exp["stepIndex"], script["stepItemArray"]);
    }
    JsonData OnContains(string index,JsonData data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] == null)
                continue;
            if (data[i]["stepIndex"].Equals(index))
                return data[i];
        }
        return null;
    }
    /// <summary>
    /// 每次切换场景使用
    /// </summary>
    public void OnClear()
    {
        if (currentExperimentData == null)
            return;
        currentExperimentData.resetButton = "";
        currentExperimentData.stepIndex = "";
        currentExperimentData.stepData.Clear();
    }
    /// <summary>
    /// 重新保存数据（用户切换步骤，进行操作之后需要清楚之后的步骤）
    /// </summary>
    public void OnRestock(string index)
    {
        Debug.Log("index：" + index);
        if (currentExperimentData == null)
        {
            Debug.Log("ExperienceNewSteps 为空");
            return;
        }
        if (currentExperimentData.stepData == null || currentExperimentData.stepData.Count <= 0)
        {
            Debug.Log("unity脚本中 步骤信息为空");
            return;
        }
        int var = -1;
        for (int i = 0; i < currentExperimentData.stepData.Count; i++)
        {
            if (currentExperimentData.stepData[i] == null)
                continue;
            Debug.Log("步骤数据++" + currentExperimentData.stepData[i].stepIndex + "用户操作步骤:" + index + "===是否包含 ：" + currentExperimentData.stepData[i].stepIndex.Equals(index));
            if (currentExperimentData.stepData[i].stepIndex.Equals(index))
            {
                var = i;
            }
        }
        if (var <= -1)
            return;
        for (int i = currentExperimentData.stepData.Count - 1; i > var; i--)
        {
            Debug.Log("删除步骤++" + i);
            currentExperimentData.stepData.RemoveAt(i);
        }
        OnSendDataToAndroid();
    }
    ///// <summary>
    ///// 重置
    ///// </summary>
    //public void OnRefresh()
    //{
    //    if (currentExperimentData == null)
    //        return;
    //    currentExperimentData.resetButton = "";
    //    currentExperimentData.stepIndex = "";
    //    currentExperimentData.stepData.Clear();
    //}
}
[SerializeField]
/// <summary>
/// ToAndroid 实验步骤按钮信息
/// </summary>
public class ExperienceNewSteps
{
    //重置
    public string resetButton;
    //小屏点了第几步
    public string stepIndex;
    //步骤
    public List<ExperimentStep> stepData;

}
[SerializeField]
/// <summary> 
/// ToAndroid   实验步骤
/// </summary>
public class ExperimentStep
{
    public string stepIndex;
    public List<ExperimentStepItem> stepItemArray;
}
[SerializeField]
//单个步骤中的物体操作
public class ExperimentStepItem
{
    public object mJson;
}