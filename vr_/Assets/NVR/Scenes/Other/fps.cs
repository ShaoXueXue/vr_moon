using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fps : MonoBehaviour {

    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fpss;

#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Start()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        Application.targetFrameRate = 30;
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Update()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fpss = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
        GetComponent<Text>().text = fpss.ToString();
    }
}
