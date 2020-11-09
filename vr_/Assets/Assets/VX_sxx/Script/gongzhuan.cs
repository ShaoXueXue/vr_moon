using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gongzhuan : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 3f;//速度
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(target.position, Vector3.up, rotateSpeed);//目标位置、旋转方向、旋转的速度
    }
}