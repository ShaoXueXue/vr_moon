using UnityEngine;
using System.Collections;

public class NOLO_TestThrow : MonoBehaviour
{

    public GameObject prefab;
    public Rigidbody attachPoint;

    private NoloVR_Controller.NoloDevice device;
    private NoloVR_TrackedDevice trackedObj;
    private FixedJoint joint;

    // Use this for initialization
    private void Start()
    {
        device = NoloVR_Controller.GetDevice(GetComponent<NoloVR_TrackedDevice>().deviceType);
    }

    private void Update()
    {
#if NOLO_3DOF || NOLO_6DOF
        if (joint == null && device.GetNoloButtonDown(NoloButtonID.Trigger))
        {
            var go = GameObject.Instantiate(prefab);
            go.transform.position = attachPoint.transform.position;

            joint = go.AddComponent<FixedJoint>();
            joint.connectedBody = attachPoint;
        }
        else if (joint != null && device.GetNoloButtonUp(NoloButtonID.Trigger))
        {

            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            Object.Destroy(go, 15.0f);

            rigidbody.velocity = device.GetPose().vecVelocity;
            rigidbody.angularVelocity = device.GetPose().vecAngularVelocity;
            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }
#endif
    }
}
