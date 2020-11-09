using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;
public class ViewNodeManager : MonoBehaviour
{
    public static ViewNodeManager singleton { get; private set; }
    public ViewNode[] viewNodeArray;

    //有些地方的虚拟相机没有挂ViewNode脚本，因为它不需要拉近拉远，
    //如实验中某个特定仪器的特写视角相机
    //但它需要有两个手柄同时外张则切换回场景相机的功能
    //因此在管理类中写个事件，在两只手柄同外张的时候调这个用事件
    public event DelegateT<float> OnHandleZoomEvent = null;

    private void Awake()
    {
        singleton = this;
        if(viewNodeArray == null || viewNodeArray.Length <= 0)
        {
            viewNodeArray = GetComponentsInChildren<ViewNode>();
        }
    }
    public ViewNode OnGetViewNode()
    {
        ViewNode script = null;
        for (int i = 0; i < viewNodeArray.Length; i++)
        {
            if (viewNodeArray[i] == null)
                continue;
            if (viewNodeArray[i].gameObject.activeInHierarchy && viewNodeArray[i].enabled)
                script = viewNodeArray[i];
        }
        return script;
    }
    #region .手柄事件
    public void OnHoldZoom(float deltaZoom)
    {
        ViewNode var = OnGetViewNode();
        OnHandleZoomEvent?.Invoke(deltaZoom);
        if (var == null)
            return;
        var.HoldZoom(deltaZoom);

    }
    public void Pan_Input(Vector3 deltaMove)
    {
        ViewNode var = OnGetViewNode();
        if (var == null)
            return;
        var.HoldPan(deltaMove);
    }
    #endregion
   

}
