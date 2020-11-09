using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour
{
    //public LineRenderer line;
    // Start is called before the first frame update
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Start()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        
    }

    // Update is called once per frame
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Update()
#pragma warning restore IDE0051 // 删除未使用的私有成员
    {
        LightFor();
    }

    private void LightFor() 
    {
        //line.SetPosition(1,Vector3.forward* 200 );
    }
}
