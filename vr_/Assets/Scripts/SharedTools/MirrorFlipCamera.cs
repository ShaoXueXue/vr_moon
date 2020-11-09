using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoloVR;
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class MirrorFlipCamera : MonoBehaviour
{
    private Camera m_cam = null;
    private Camera m_Camera
    {
        get
        {
            if (m_cam == null)
            {
                Initial();
            }
            return m_cam;
        }
    }
    private bool m_Initialed = false;

    public static bool IsFlipY { get; private set; }
    public static bool IsFlipX { get; private set; }

    //public static MirrorFlipCamera singleton
    //{
    //    get
    //    {
    //        return _singleton;
    //    }
    //}
    //static MirrorFlipCamera _singleton;

    private void Awake()
    {
        //_singleton = this;
    }
    private void Start()
    {
#if !UNITY_EDITOR
        MirrorY();
#endif
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MirrorY();
        }
    }

    public void MirrorX()
    {
        IsFlipX = !IsFlipX;
        AutoPlaceDevices.Instance?.PlaceDevices();
    }
    public void MirrorY()
    {
        IsFlipY = !IsFlipY;
        //true 为颠倒
    }

    private void Initial()
    {
        if (m_Initialed) return;
        m_cam = GetComponent<Camera>();
        //m_Camera = Camera.main;
        IsFlipY = IsFlipX = false;
        MirrorY();
    }
    private void OnPreCull()
    {
        m_Camera.ResetWorldToCameraMatrix();
        m_Camera.ResetProjectionMatrix();
        Vector3 scale = new Vector3(IsFlipX ? -1 : 1, IsFlipY ? -1 : 1, 1);
        m_Camera.projectionMatrix = m_Camera.projectionMatrix * Matrix4x4.Scale(scale);
    }
    private void OnPreRender()
    {
        GL.invertCulling = IsFlipX ^ IsFlipY;
    }
    private void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
