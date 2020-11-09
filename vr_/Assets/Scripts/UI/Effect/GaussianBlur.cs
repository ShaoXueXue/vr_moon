using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 具有遮罩的图片材质修改(高斯模糊)
/// </summary>
public class GaussianBlur : MonoBehaviour
{
    [SerializeField]
#pragma warning disable IDE0044 // 添加只读修饰符
    private CustomImage _image=null;
#pragma warning restore IDE0044 // 添加只读修饰符
    public Image mImage
    {
        get
        {
            return _image;
        }
    }
    [SerializeField]
#pragma warning disable IDE0044 // 添加只读修饰符
    private float GaussianBlurMax=0;
#pragma warning restore IDE0044 // 添加只读修饰符
    [SerializeField]
#pragma warning disable IDE0044 // 添加只读修饰符
    private float GaussianBlurMin=0;
#pragma warning restore IDE0044 // 添加只读修饰符
    private Material gaussianBlurMaterial=null;
    private float defaultValue=0;
    public void OnInit()
    {
        gaussianBlurMaterial = mImage.GetModifiedMaterial(mImage.material);
        defaultValue = gaussianBlurMaterial.GetFloat("_Size");
    }
    public void SetMaterialGaussianBlur(float var)
    {
        float value = (GaussianBlurMax - GaussianBlurMin) * var + GaussianBlurMin;
        if (gaussianBlurMaterial != null)
            gaussianBlurMaterial.SetFloat("_Size", value);
    }
    public void OnRefresh()
    {
        if (gaussianBlurMaterial != null)
            gaussianBlurMaterial.SetFloat("_Size", defaultValue);
    }
}
