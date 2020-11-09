using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoloVR
{
    public class NoloVR_Model_Manager : MonoBehaviour
    {
#if NOLO_6DOF
        [SerializeField]
        private GameObject ThreeDF = null;
#endif

#if NOLO_6DOF
        private GameObject SixDF=null; 
#elif NOLO_3DOF
        private void Start()
        {
#if NOLO_6DOF
            SixDF.SetActive(true);
#endif
#elif NOLO_3DOF
            ThreeDF.SetActive(true);
#endif
        }
    }
}

