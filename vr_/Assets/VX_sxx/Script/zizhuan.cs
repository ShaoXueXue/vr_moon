using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zizhuan : MonoBehaviour
{
    public float rotateSpeed = 2f;//速度
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateSpeed);//旋转角度
    }
}

