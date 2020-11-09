using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class NoloHandleRayLine : MonoBehaviour
{
    Image line;
    public Transform orgin;
    public Transform end;
    private void Awake()
    {
        if (line == null)
            line = GetComponent<Image>();
        if (orgin == null)
            orgin = this.transform;
    }
    private void Update()
    {
        if (end == null || !end.gameObject.activeSelf)
            return;
        Vector3 targetPos = end.localPosition;
        Vector3 curPos = transform.localPosition;
        line.rectTransform.sizeDelta = new Vector2(8, Vector3.Distance(targetPos, curPos));

        //设置角度；
        double angle = Math.Atan2(targetPos.y - curPos.y, targetPos.x - curPos.x) * 180 / Math.PI;
        line.transform.rotation = Quaternion.Euler(0, 0, (float)angle + 270);
    }
}
