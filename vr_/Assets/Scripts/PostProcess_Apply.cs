using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
/// <summary>
/// 后台处理应用
/// </summary>
namespace VisualEffect
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class PostProcess_Apply : MonoBehaviour
    {
        public static PostProcess_Apply Instance { get; private set; }
        private PostProcessVolume m_PostProcessVolume;
        private PostProcessVolume mPostProcessVolume {
            get {
                if (m_PostProcessVolume == null)
                {
                    m_PostProcessVolume = GetComponent<PostProcessVolume>();
                }
                return m_PostProcessVolume;
            }
        }
#pragma warning disable IDE0051 // 删除未使用的私有成员
        private void Awake()
#pragma warning restore IDE0051 // 删除未使用的私有成员
        {
            Instance = this;
        }
        // Update is called once per frame
        public void SetPostProcessProfile(PostProcessProfile profile)
        {
            mPostProcessVolume.sharedProfile = profile;
        }
    }
}