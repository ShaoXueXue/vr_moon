using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))//按下A键太阳放大
        {
            ani.SetTrigger("sun_big");
        }
        if (Input.GetKey(KeyCode.Z))//按下Z键太阳回到默认状态
        {
            ani.SetTrigger("sun_moren");
        }

        if (Input.GetKey(KeyCode.S))//按下S键月球放大
        {
            ani.SetTrigger("moon_big");
        }
        if (Input.GetKey(KeyCode.X))//按下X键月球回到默认状态
        {
            ani.SetTrigger("moon_moren");
        }

        if (Input.GetKey(KeyCode.D))//按下D键地球放大
        {
            ani.SetTrigger("earth_big");
        }
        if (Input.GetKey(KeyCode.C))//按下C键地球回到默认状态
        {
            ani.SetTrigger("earth_moren");
        }
    }
}
