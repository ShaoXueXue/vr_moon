using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 参考工程
/// </summary>
public class DemoSceneMgr : IBaseScene
{
    [Header("默认场景ID")]
    public int startSceneIndex;
    //public Text text;
    IBaseItemScene[] itemSceneArray;
    [HideInInspector]
    public int[] LabSource;
    static DemoSceneMgr _singleton;
    public static DemoSceneMgr singleton
    {
        get
        {
            return _singleton;
        }
    }
    public override void OnStartSceneEnter()
    {
        base.OnStartSceneEnter();
        if (itemSceneArray == null || itemSceneArray.Length <= 0)
        {
            itemSceneArray = GetComponentsInChildren<IBaseItemScene>();
        }
        for (int i = 0; i < itemSceneArray.Length; i++)
        {
            if (itemSceneArray == null)
                continue;
            itemSceneArray[i].OnInit();
            itemSceneArray[i].OnToggle(startSceneIndex);
        }
        LabSource = new int[4];
        for (int i = 0; i < LabSource.Length; i++)
        {
            LabSource[i] = 0;
        }
        //OnSelectScene(startSceneIndex);
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        _singleton = this;
    }
    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    protected override void OnGetButtonIDByAndroid(int index)
    {
        if (index != 100)
        {
            base.OnGetButtonIDByAndroid(index);
            //text.text = "";
            for (int i = 0; i < itemSceneArray.Length; i++)
            {
                if (itemSceneArray == null)
                    continue;
                itemSceneArray[i].OnToggle(startSceneIndex);
            }
        }
        else
        {
            int source = OnCalculateLabSource();
            //text.text = "当前实验获取分数：" + source;
        }
    }
    protected override int OnCalculateLabSource()
    {
        int allSource = 0;
        for (int i = 0; i < LabSource.Length; i++)
        {
            allSource += LabSource[i];
        }
        return allSource;
    }
    protected override List<string> OnSmallStepsOfExperiment()
    {
        return base.OnSmallStepsOfExperiment();
    }
    public override void OnRefresh()
    {
        base.OnRefresh();
        for (int i = 0; i < LabSource.Length; i++)
        {
            LabSource[i] = 0;
        }
    }
    public override void OnDestaryThisLab()
    {
        base.OnDestaryThisLab();    
    }
    //void OnSelectScene(int index)
    //{
    //    for (int i = 0; i < itemSceneArray.Length; i++)
    //    {
    //        if (itemSceneArray[i] == null)
    //            continue;
    //        itemSceneArray[i].OnToggle(index);
    //    }
    //}
    /*模拟安卓按钮交互，UI按钮则为安卓按钮*/
    public void OnImitationAndroid(int index)
    {
        OnGetButtonIDByAndroid(index);
    }
}
