using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoloVR
{
    public class AutoPlaceDevices : MonoBehaviour
    {
        public static AutoPlaceDevices Instance { get; private set; }

        private const float m_Distance = 0.06f;
        private const float m_OffsetDeg = 8f;
        private Vector3 m_Midpoint = new Vector3(0f, -0.035f, 0.15f);
        private TrackedDevice[] m_Devices;
#pragma warning disable IDE0044 // 添加只读修饰符
#pragma warning disable IDE0051 // 删除未使用的私有成员
        private Camera m_Camera;
#pragma warning restore IDE0051 // 删除未使用的私有成员
#pragma warning restore IDE0044 // 添加只读修饰符

#pragma warning disable IDE0051 // 删除未使用的私有成员
        private void Awake()
#pragma warning restore IDE0051 // 删除未使用的私有成员
        {
            Initial();
        }

        private void Initial()
        {
            Instance = this;
            m_Devices = GetComponentsInChildren<TrackedDevice>();
            AutoDevicePos();

        }

        public void AutoDevicePos()
        {
            StartCoroutine(SetLater());
        }

        public void PlaceDevices()
        {
            Vector3 devicePos_L = m_Midpoint + Vector3.left * m_Distance * 0.5f;
            Vector3 devicePos_R = m_Midpoint + Vector3.right * m_Distance * 0.5f;

            foreach (TrackedDevice dev in m_Devices)
            {
                if (dev.DeviceType == NoloDeviceType.LeftController ^ MirrorFlipCamera.IsFlipX)
                {
                    dev.transform.localPosition = devicePos_L;
                    dev.OffsetQuat = Quaternion.AngleAxis(-m_OffsetDeg, Vector3.up);
                }
                else if (dev.DeviceType == NoloDeviceType.RightController ^ MirrorFlipCamera.IsFlipX)
                {
                    dev.transform.localPosition = devicePos_R;
                    dev.OffsetQuat = Quaternion.AngleAxis(m_OffsetDeg, Vector3.up);
                }
            }
        }

        private IEnumerator SetLater()
        {
            yield return null;
            yield return null;
            yield return null;
            PlaceDevices();
        }
    }
}

