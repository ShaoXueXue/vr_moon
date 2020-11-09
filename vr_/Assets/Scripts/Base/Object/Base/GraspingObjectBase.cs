using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Public;
using cakeslice;
/// <summary>
/// 具有物理效果，行为的物体父类
/// </summary>
public class GraspingObjectBase : MonoBehaviour
{
    /// <summary>
    /// 器材ID
    /// </summary>
    public List<int> entityID = new List<int>();   
    protected Transform DeviceTrans;    //自身
    protected Transform cameraTra;
    protected const float m_PickOffset_z = 0f;
    protected Vector3[] m_IntersectPlane;
    protected Vector3[] m_IntersectPlanePositive = new Vector3[3] { Vector3.zero, Vector3.up, Vector3.right };
    protected Vector3[] m_IntersectPlaneNegative = new Vector3[3] { Vector3.zero, Vector3.up, Vector3.left };
    protected NoloDeviceType mHandleType;
    public GameObjectType objectType;
    public GameObject rayObj;
    [HideInInspector]
    protected bool isPut;  //是否被放下
    public bool isHandle { get; set; }    //是否被拿起
    public float selfMass = 0;       //物体质量
    protected Transform defaultParent;
    protected Vector3 defaultLoacalPos;
    public Vector3 mDefaultLocalPos
    {
        get
        {
            return defaultLoacalPos;
        }
    }
    protected Vector3 defaultWorldRot;
    public Vector3 mDefaultLocalRot
    {
        get
        {
            return defaultWorldRot;
        }
    }
    [Header("被拾取时的旋转")]
    [SerializeField]
    protected Vector3 targetRot=Vector3.zero;
    [Header("描边")]
    [SerializeField]
    protected Outline[] outLine;

    [Header("边界界限")]
    [SerializeField]
    Transform upTransform;
    public Transform mUpTrasnform { get { return upTransform; } }
    [SerializeField]
    Transform downTransform;
    public Transform mDownTrasnform { get { return downTransform; } }
    [SerializeField]
    Transform leftTransform;
    public Transform mLeftTrasnform { get { return leftTransform; } }
    [SerializeField]
    Transform rightTransform;
    public Transform mRightTrasnform { get { return rightTransform; } }
    
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void OnInit()
    {
        if (outLine.Length == 0)
            outLine = GetComponentsInChildren<Outline>();
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].enabled = false;
            outLine[i].color = 0;
        }
        isPut = true;
        isHandle = false;
        isCountDown = false;
        currentTime = 0;
        defaultParent = transform.parent;
     
        defaultLoacalPos = transform.localPosition;
        defaultWorldRot = transform.eulerAngles;
        if (DeviceTrans == null)
            DeviceTrans = transform;

    }

    /// <summary>
    /// 拾取
    /// </summary>
    /// <param name="camera"></param>
    public virtual void OnHandleTrigger(Transform camera, NoloDeviceType handleType)
    {
        mHandleType = handleType;
        transform.eulerAngles = targetRot;
        isPut = false;
        cameraTra = camera;
        m_IntersectPlane = (cameraTra.position.z < 0) ? m_IntersectPlanePositive : m_IntersectPlaneNegative;

        isCountDown = false;
        currentTime = 0;

        if (entityID != null && entityID.Count > 0)
            DataTools.SendImageToAndroid(entityID); 
        OnPickUp();
    }
    /// <summary>
    /// 拾取中
    /// </summary>
    /// <param name="newRay"></param>
    public virtual void OnHoldingTrigger(Ray newRay)
    {
        //Debug.LogError(DeviceTrans);
        if (DeviceTrans == null) return;
        isHandle = true;
        Ray deviceRay = newRay;
        Vector3 hitPos;
        if (RayTriInsecPos(m_IntersectPlane[0], m_IntersectPlane[1], m_IntersectPlane[2], deviceRay, out hitPos))
        {
            for (int i = 0; i < outLine.Length; i++)
            {
                if (outLine[i] == null)
                    continue;
                outLine[i].enabled = true;
                outLine[i].color = 1;
            }
            RaycastHit interactHit;
            if (Physics.Raycast(DeviceTrans.position, DeviceTrans.forward, out interactHit, 1000))
            {
                float hitZ = interactHit.point.z;
                //Debug.LogError(hitZ);
                float offsetZ = (hitZ > 0) ? hitZ + m_PickOffset_z : m_PickOffset_z;
                transform.position = hitPos + Vector3.forward * offsetZ;
            }
            else
                transform.position = hitPos + Vector3.forward * m_PickOffset_z;

        }
    }
    public virtual void OnExitScript()
    {

    }

    /// <summary>
    /// 放下
    /// </summary>
    public virtual void OnReleseTrigger(NoloDeviceType handleType)
    {
        isPut = true;
        mHandleType = handleType;
        SetParent();
        OnPutDown();
        transform.eulerAngles = defaultWorldRot;
        isCountDown = true;
        currentTime = 0;
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].color = 0;
            outLine[i].enabled = false;
        }
        if (this.transform.localPosition.y < -1f)
        {
            Debug.Log("移出太远");
            this.transform.localPosition = defaultLoacalPos;
        }
    }
    /// <summary>
    /// 是否拾取
    /// </summary>
    /// <returns></returns>
    public bool OnSetIfPutDown()
    {
        return isPut;
    }
    /// <summary>
    /// 被手捡起
    /// </summary>
    public virtual void OnPickUp()
    {
       
    }
    /// <summary>
    /// 被手放下
    /// </summary>
    public virtual void OnPutDown()
    { 
    }
    /// <summary>
    /// 将物体放入物体上面
    /// </summary>
    public virtual void OnPutItOnTheObject()
    {

    }
    [HideInInspector]
    public Transform parentTrans;
    public void SetParent(Transform other = null)
    {
        parentTrans = other;
        if (other == null)
            transform.SetParent(defaultParent);
        else
            transform.SetParent(other);
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void OnRefresh()
    {
        SetParent();
        this.transform.localPosition = defaultLoacalPos;
        this.transform.eulerAngles = defaultWorldRot;
        OnPutDown();
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].enabled = false;
            outLine[i].color = 0;
        }
        isHandle = false;
        isPut = true;
        isCountDown = false;
        currentTime = 0;
    }
    /// <summary>
    /// 切换场景还原物体拾取之前信息
    /// </summary>
    public virtual void OnRefreshHandle()
    {

    }
    private void OnDestroy()
    {
        OnDestroyThis();
    }
    /// <summary>
    /// OnDestroy
    /// </summary>
    protected virtual void OnDestroyThis()
    {

    }
    /// <summary>
    /// 清扫初始化
    /// </summary>
    public virtual void OnSweep()
    {

    }
    public virtual void OnRayEnter()
    {
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].enabled = true;
            outLine[i].color = 0;
        }
    }
    public virtual void OnRayExit()
    {
        for (int i = 0; i < outLine.Length; i++)
        {
            if (outLine[i] == null)
                continue;
            outLine[i].color = 0;
            outLine[i].enabled = false;
        }
    }
    protected bool isCountDown;  //是否开始计时
    const float ratio = 2.5f;  //物体3秒内必须进行吸附操作
    protected float currentTime;   //当前时间
    private void Update()
    {
        if (isCountDown)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= ratio)
            {
                isHandle = false;
                currentTime = ratio;
                isCountDown = false;
            }
        }
    }
    #region .数据转换
    public bool RayTriInsecPos(Vector3 p0, Vector3 p1, Vector3 p2, Ray ray, out Vector3 InsecPos)
    {
        Debug.DrawRay(ray.origin, ray.direction, Color.blue);
        Vector3 fn = Vector3.Cross(p1 - p0, p2 - p0);//faceNormal
        Vector3 rd = ray.direction;
        Vector3 ro = ray.origin;
        if (Vector3.Dot(fn, rd) >= 0)
        {
            InsecPos = Vector3.zero;
            return false;
        }
        float t = ((p0.x - ro.x) * fn.x + (p0.y - ro.y) * fn.y + (p0.z - ro.z) * fn.z) / (fn.x * rd.x + fn.y * rd.y + fn.z * rd.z);
        InsecPos = ro + rd * t;
        DrawATriangle(p0, p1, p2);
        return true;
    }
    //Debug
    public void DrawATriangle(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        //Debug.DrawLine(p0, p1, Color.green, 10);
        //Debug.DrawLine(p1, p2, Color.red, 10);
        //Debug.DrawLine(p2, p0, Color.yellow, 10);
        //Debug.DrawRay();
     
    }
    #endregion
}
