using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedObjControler : MonoBehaviour
{
    public static SharedObjControler Instance { private set; get; }
    [SerializeField] private cakeslice.OutlineEffect m_OutlineEffect=null;
    public cakeslice.OutlineEffect OutlineEffect => m_OutlineEffect;
    private void Awake()
    {
        Instance = this;
    }

}
