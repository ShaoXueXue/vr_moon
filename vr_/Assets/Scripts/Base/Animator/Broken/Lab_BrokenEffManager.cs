using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;

//破碎管理
public class Lab_BrokenEffManager : MonoBehaviour
{
    [SerializeField]
    private BrokenEff[] brokenEffArray=null;
    public event DelegateT OnRestoreEvent=null;
    public void OnInit()
    {
        for (int i = 0; i < brokenEffArray.Length; i++)
        {
            if (brokenEffArray[i] == null)
                continue;
            brokenEffArray[i].OnRestoreEvent += BrokenEvent;
            brokenEffArray[i].OnInit();
        }
    }
    private void OnDestroy()
    {
        for (int i = 0; i < brokenEffArray.Length; i++)
        {
            if (brokenEffArray[i] == null)
                continue;
            brokenEffArray[i].OnRestoreEvent -= BrokenEvent;
        }
    }
    public void OnRefresh()
    {
        OnRestoreEvent?.Invoke();
    }

    private void BrokenEvent()
    {
        OnRestoreEvent?.Invoke();
    }

    public BrokenEff OnGetBrokenEff(BrokenEffObjType type = BrokenEffObjType.DefaultState)
    {
        if (brokenEffArray.Length == 1)
            return brokenEffArray[0];
        for (int i = 0; i < brokenEffArray.Length; i++)
        {
            if (brokenEffArray[i] == null)
                continue;
            if (brokenEffArray[i].effBokenType == type)
                return brokenEffArray[i];
        }
        return null;
    }
}
