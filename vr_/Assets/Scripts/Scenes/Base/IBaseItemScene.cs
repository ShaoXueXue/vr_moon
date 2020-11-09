using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实验中的子实验基类
/// </summary>
public class IBaseItemScene : MonoBehaviour
{
    [Tooltip("实验ID")]
    public int sceneIndex;
    [SerializeField]
    GameObject cameraObj;
    [SerializeField]
    GameObject sceneObj;
    [Tooltip("拾取物体")]
    [SerializeField]
    private GraspingObjectBase[] graspingObjectBase = null;
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void OnInit()
    {
        if (graspingObjectBase == null || graspingObjectBase.Length <= 0)
        {
            graspingObjectBase = GetComponentsInChildren<GraspingObjectBase>();
        }
        if(graspingObjectBase != null && graspingObjectBase.Length > 0)
        {
            for (int i = 0; i < graspingObjectBase.Length; i++)
            {
                if (graspingObjectBase[i] == null)
                    continue;
                graspingObjectBase[i].OnInit();
            }
        }
    }
    /// <summary>
    /// 刷新
    /// </summary>
    public virtual void OnRefresh()
    {
        if (graspingObjectBase != null && graspingObjectBase.Length > 0)
        {
            for (int i = 0; i < graspingObjectBase.Length; i++)
            {
                if (graspingObjectBase[i] == null)
                    continue;
                graspingObjectBase[i].OnRefresh();
            }
        }
    }
    /// <summary>
    /// 选取该实验（在Init之后才可以调用）
    /// </summary>
    /// <param name="index"></param>
    public virtual void OnToggle(int index)
    {
        if (cameraObj != null)
            cameraObj.SetActive(index == sceneIndex);
        if (sceneObj != null)
            sceneObj.SetActive(index == sceneIndex);
    }
}
