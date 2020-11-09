using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Public;
//视野功能
public class ViewNode : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook cinemachineFreeLook=null;
   	[Header("自由相机下向前拉近的最近距离")]
    [SerializeField]
    private float m_Distance_Min=0;
    public float mDistance_Min { set { m_Distance_Min = value; } get { return m_Distance_Min; } }
    [Header("自由相机下向后拉远的最远距离")]
    [SerializeField]
    private float m_Distance_Max=0;
    public float mDistance_Max { set { m_Distance_Max = value; } get { return m_Distance_Max; } }
    public CinemachineFreeLook mCinemachineFreeLook { get { return cinemachineFreeLook; } }
    private CinemachineFreeLook.Orbit[] m_OriginalOrbits=null;

    public CinemachineFreeLook.Orbit[] mOriginalOrbits { set { m_OriginalOrbits = value; } }
    private float m_CamDisFactor=0;
    public float mCamDisFactor { set { m_CamDisFactor = value; } }
    public event DelegateT<float> OnHoldZoomEvent=null;

    //新增，虚拟相机m_XAxis和m_YAxis默认值
    private float m_XAxis = 0, m_YAxis;



    //新增固定虚拟相机
    public CinemachineVirtualCamera virtualCamera=null;
    private float m_OriginaLensFieldOfView=0;

    //是否是固定虚拟相机
    public bool isVirtual = false;
    public void OnState(bool state)
    {
        transform.gameObject.SetActive(state);

    }
    private void Awake()
    {
        if (isVirtual)
        {
            m_OriginaLensFieldOfView = virtualCamera.m_Lens.FieldOfView;
        }
        else 
        {
            m_OriginalOrbits = new CinemachineFreeLook.Orbit[cinemachineFreeLook.m_Orbits.Length];
            for (int i = 0; i < cinemachineFreeLook.m_Orbits.Length; i++)
            {
                m_OriginalOrbits[i].m_Height = cinemachineFreeLook.m_Orbits[i].m_Height;
                m_OriginalOrbits[i].m_Radius = cinemachineFreeLook.m_Orbits[i].m_Radius;
            }
            float camDis = m_OriginalOrbits[1].m_Radius;
            m_CamDisFactor = 1 / camDis;//Camera Radius Scale factor
            m_AnchorOriginalPos = AnchorTrans.localPosition;
            m_TargOriginalPos = m_TargTrans.localPosition;
            //新增虚拟相机的默认高度和角度
            m_XAxis = cinemachineFreeLook.m_XAxis.Value;
            m_YAxis = cinemachineFreeLook.m_YAxis.Value;
        }



    }

    private void OnEnable()
    {
       
        //刷新缩放
        if (!isVirtual)
        {
            //Debug.Log("OnEnable");
            //刷新拖动
            AnchorTrans.localPosition = m_AnchorOriginalPos;
            m_TargTrans.localPosition = m_TargOriginalPos;
            for (int i = 0; i < m_OriginalOrbits.Length; i++)
            {
                cinemachineFreeLook.m_Orbits[i].m_Height = m_OriginalOrbits[i].m_Height;
                cinemachineFreeLook.m_Orbits[i].m_Radius = m_OriginalOrbits[i].m_Radius;
            }
            
            //修改，这里不能粗暴的把它角度和高度置为0，要和三个半径一样还原回原来默认值
            //cinemachineFreeLook.m_XAxis.Value = 0;
            //cinemachineFreeLook.m_YAxis.Value = 0;
            cinemachineFreeLook.m_XAxis.Value = m_XAxis;
            cinemachineFreeLook.m_YAxis.Value = m_YAxis;
        }
        else if (isVirtual)
        {
            virtualCamera.m_Lens.FieldOfView = m_OriginaLensFieldOfView;
        }
    }

    private void OnDisable()
    {
        if (isVirtual)
            return;
        //Debug.Log("OnDisable");
        for (int i = 0; i < m_OriginalOrbits.Length; i++)
        {
            cinemachineFreeLook.m_Orbits[i].m_Height = m_OriginalOrbits[i].m_Height;
            cinemachineFreeLook.m_Orbits[i].m_Radius = m_OriginalOrbits[i].m_Radius;
        }
       
        //修改，这里不能粗暴的把它角度和高度置为0，要和三个半径一样还原回原来默认值
        //cinemachineFreeLook.m_XAxis.Value = 0;
        //cinemachineFreeLook.m_YAxis.Value = 0;
        cinemachineFreeLook.m_XAxis.Value = m_XAxis;
        cinemachineFreeLook.m_YAxis.Value = m_YAxis;
    }
    #region .控制缩放
    private const float m_ZoomFactor = 1f;

    public void HoldZoom(float normalizeZoomOut)
    {
        normalizeZoomOut *= m_ZoomFactor;
        if (!isVirtual)
        {
            if (mCinemachineFreeLook.gameObject.activeSelf==false) 
            {
                return;
            }
        	float zoomProp = Mathf.InverseLerp(m_Distance_Min, m_Distance_Max, cinemachineFreeLook.m_Orbits[1].m_Radius);
            zoomProp += normalizeZoomOut;

        	if (zoomProp > 1)
        	{
            	zoomProp = 1;
        	}
        	else if (zoomProp < 0)
        	{
            	zoomProp = 0;
        	}
            //Debug.Log("缩放系数：" + zoomProp);
            OnHoldZoomEvent?.Invoke(zoomProp);

            float distance = Mathf.Lerp(m_Distance_Min, m_Distance_Max, zoomProp);
        	float camRadiusScale = distance * m_CamDisFactor;
        	for (int i = 0; i < m_OriginalOrbits.Length; i++)
        	{
                cinemachineFreeLook.m_Orbits[i].m_Height = m_OriginalOrbits[i].m_Height * camRadiusScale;
                cinemachineFreeLook.m_Orbits[i].m_Radius = m_OriginalOrbits[i].m_Radius * camRadiusScale;
            }
    	}
        else
        {
            float view = virtualCamera.m_Lens.FieldOfView;
            view += normalizeZoomOut * 10;

            if (view >= 30 && view <= 50) 
            {
                virtualCamera.m_Lens.FieldOfView= view;
            }
            else if (view < 30) 
            {
                virtualCamera.m_Lens.FieldOfView = 30;
            }
            else if (view > 50)
            {
                virtualCamera.m_Lens.FieldOfView = 50;
            }
        }
    }
    #endregion

    #region .视野移动
    [Header("跟随物体")]
    [Header("视野移动")]
    
    [SerializeField]
    private Transform AnchorTrans=null;

    [Header("朝向物体")]
    [SerializeField]
    private Transform m_TargTrans=null;

	[Header("（X轴）是否具有视野移动功能")]
    [SerializeField]
    private bool m_UsePan = false;    //是否具有视野移动功能

    [Header("（Y轴）是否具有视野移动功能")]
    [SerializeField]

    private bool m_UsePan_Y = false;


    [SerializeField]

    private float m_PanFactor = 1f;


    [SerializeField]
    private Vector4 m_PanRect = new Vector4(-5, 0, 5, 5); //relatively to  Original

    private Vector3 m_AnchorOriginalPos=Vector3.zero;//Default Pos
    private Vector3 m_TargOriginalPos = Vector3.zero;//Default Pos
    public void HoldPan(Vector3 deltaMove)
    {
        if (!m_UsePan) 
            return;
        deltaMove *= -m_PanFactor * Time.deltaTime;
        float anchor_X = Mathf.Clamp(AnchorTrans.localPosition.x + deltaMove.x, m_AnchorOriginalPos.x + m_PanRect.x, m_AnchorOriginalPos.x + m_PanRect.z);
        float anchor_Y = m_UsePan_Y ? Mathf.Clamp(AnchorTrans.localPosition.y + deltaMove.y, m_AnchorOriginalPos.y + m_PanRect.y, m_AnchorOriginalPos.y + m_PanRect.w) :
                         AnchorTrans.localPosition.y;
        float anchor_Z = AnchorTrans.localPosition.z;
        AnchorTrans.localPosition = new Vector3(anchor_X, anchor_Y, anchor_Z);


        float target_X = Mathf.Clamp(m_TargTrans.localPosition.x + deltaMove.x, m_TargOriginalPos.x + m_PanRect.x, m_TargOriginalPos.x + m_PanRect.z);
        float target_Y = m_UsePan_Y ? Mathf.Clamp(m_TargTrans.localPosition.y + deltaMove.y, m_TargOriginalPos.y + m_PanRect.y, m_TargOriginalPos.y + m_PanRect.w) :
                         m_TargTrans.localPosition.y;
        float target_Z = m_TargTrans.localPosition.z;

        m_TargTrans.localPosition = new Vector3(target_X, target_Y, target_Z);
    }
    #endregion
    public void OnRefresh()
    {
       
        //刷新缩放
        if (isVirtual)
        {
            virtualCamera.m_Lens.FieldOfView = m_OriginaLensFieldOfView;
        }
        else
        {
            //刷新拖动
            AnchorTrans.localPosition = m_AnchorOriginalPos;
            m_TargTrans.localPosition = m_TargOriginalPos;
            for (int i = 0; i < m_OriginalOrbits.Length; i++)
            {
                cinemachineFreeLook.m_Orbits[i].m_Height = m_OriginalOrbits[i].m_Height;
                cinemachineFreeLook.m_Orbits[i].m_Radius = m_OriginalOrbits[i].m_Radius;
            }
            cinemachineFreeLook.m_XAxis.Value = 0;
            cinemachineFreeLook.m_YAxis.Value = 0;
        }
    }
}
