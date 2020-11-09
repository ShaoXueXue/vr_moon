using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoloVR_Model_Manager : MonoBehaviour {
#pragma warning disable IDE0051 // 删除未使用的私有成员
    private void Start () {
#pragma warning restore IDE0051 // 删除未使用的私有成员
#if NOLO_6DOF
         if (transform.Find("NOLO_Controller") != null)
        {
            transform.Find("NOLO_Controller").gameObject.SetActive(true);
        }
#elif NOLO_3DOF
         if (transform.Find("NOLO_Controller_C1") != null)
        {
            transform.Find("NOLO_Controller_C1").gameObject.SetActive(true);
        }
#endif
    }
}
