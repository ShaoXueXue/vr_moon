using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 死亡脚本
/// </summary>
public class DeathGround2D : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        GraspingObjectBase script = collision.GetComponent<GraspingObjectBase>();
        if (script == null)
            return;
        script.transform.localPosition = script.mDefaultLocalPos;
        script.isHandle = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GraspingObjectBase script = collision.transform.GetComponent<GraspingObjectBase>();
        if (script == null)
            return;
        script.transform.localPosition = script.mDefaultLocalPos;
        script.isHandle = false;
    }
}
