using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;

//可以公共使用

public class BrokenEff : MonoBehaviour
{
    [SerializeField]
#pragma warning disable IDE0044 // 添加只读修饰符
    private Transform brokenEffObj=null;
#pragma warning restore IDE0044 // 添加只读修饰符
    public BrokenEffObjType effBokenType= BrokenEffObjType.DefaultState;
    public Transform[] m_partical;
    public event DelegateT OnRestoreEvent=null;
    Transform FireBroken;
    Transform m_FireBroken;
    [SerializeField]
    [HideInInspector] 
    private Collider[] m_ColPieces=null;
    [SerializeField] 
    [HideInInspector] 
    private PiecesAttributes[] m_PiecesAttributes=null;
    private Dictionary<Collider, PiecesAttributes> m_Col_Pieces=new Dictionary<Collider, PiecesAttributes>();
    private int m_PickedAmount = 0;
    public void OnInit()
    {
        m_Col_Pieces = new Dictionary<Collider, PiecesAttributes>();
        brokenEffObj.gameObject.SetActive(false);
        m_ColPieces = brokenEffObj.GetComponentsInChildren<Collider>();
        m_PiecesAttributes = new PiecesAttributes[m_ColPieces.Length];
        
        for (int i = 0; i < m_ColPieces.Length; i++)
        {
            m_PiecesAttributes[i] = new PiecesAttributes(m_ColPieces[i]);
            m_Col_Pieces.Add(m_ColPieces[i], m_PiecesAttributes[i]);
        }
    }
    public void OnBroken()
    {
        brokenEffObj.gameObject.SetActive(true);
    }

    public void OnNotFireBroken()
    {
        for (int i = 0; i < m_partical.Length; i++)
        {
            m_partical[i].gameObject.SetActive(false);
        }
        brokenEffObj.gameObject.SetActive(true);
    }
    public void OnRestore()
    {
        brokenEffObj.gameObject.SetActive(false);
        if (m_partical!=null)
        {
            for (int i = 0; i < m_partical.Length; i++)
            {
                m_partical[i].gameObject.SetActive(true);
            }
        }
      
        if (m_Col_Pieces == null || m_Col_Pieces.Count <= 0)
            return;
        foreach(var item in m_Col_Pieces)
        {
            if (item.Key == null || item.Value == null)
                continue;
            item.Value.OnRest();
        }
        m_PickedAmount = 0;
    }
    public void CleanOne_Start(Collider col)
    {
        if (m_Col_Pieces[col].HasCleaned == true) return;

        StartCoroutine(Clean_One(col));
    }
    private IEnumerator Clean_One(Collider col)
    {

        yield return new WaitForSeconds(0.2f);

        m_Col_Pieces[col].Cleaned();
        m_PickedAmount++;

        if (m_PickedAmount == m_ColPieces.Length)
        {
            OnRestoreEvent?.Invoke();
        }
    }
}
[System.Serializable]
public class PiecesAttributes
{
    [SerializeField] 
    [HideInInspector] 
    private Vector3 m_StartLocalPos;
    [SerializeField] 
    [HideInInspector] 
#pragma warning disable IDE0044 // 添加只读修饰符
    private GameObject m_GameObject;
#pragma warning restore IDE0044 // 添加只读修饰符
    [SerializeField]
    [HideInInspector] 
    public bool HasCleaned;

    public PiecesAttributes(Collider col)
    {
        HasCleaned = false;
        m_StartLocalPos = col.transform.localPosition;
        m_GameObject = col.gameObject;
    }
    public void OnRest()
    {
        HasCleaned = false;
        m_GameObject.transform.localPosition = m_StartLocalPos;
        m_GameObject.SetActive(true);
    }
    public void Cleaned()
    {
        HasCleaned = true;
        m_GameObject.SetActive(false);
    }

}
