using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math_Tools
{
    public static bool RayTriInsecPos(Vector3 p0, Vector3 p1, Vector3 p2, Ray ray,out Vector3 InsecPos)
    {
        Vector3 fn = Vector3.Cross(p1 - p0, p2 - p0);//faceNormal
        Vector3 rd = ray.direction;
        Vector3 ro = ray.origin;

        if (Vector3.Dot(fn, rd) >= 0)
        {
            InsecPos = Vector3.zero;
            return false;

        }

        //float t = 0;  //insetPos = ro  + rd * t;  //fn.x * (insetPos.x - p0.x) + fn.y * (insetPos.y - p0.y) + fn.z * (insetPos.z - p0.z) = 0; //calculation process
        float t = ((p0.x - ro.x) * fn.x + (p0.y - ro.y) * fn.y + (p0.z - ro.z) * fn.z) / (fn.x * rd.x + fn.y * rd.y + fn.z * rd.z);
        InsecPos = ro + rd * t;
        return true;
    }

    public static bool RayTriInsecIsIn(Vector3 p0, Vector3 p1, Vector3 p2, Ray ray, out Vector3 insetPos)
    {
        Vector3 fn = Vector3.Cross(p1 - p0, p2 - p0);//faceNormal
        Vector3 ro = ray.origin;
        Vector3 rd = ray.direction;
        //float t = 0;  //insetPos = ro  + rd * t;  //fn.x * (insetPos.x - p0.x) + fn.y * (insetPos.y - p0.y) + fn.z * (insetPos.z - p0.z) = 0; //calculation process

        float t = ((p0.x - ro.x) * fn.x + (p0.y - ro.y) * fn.y + (p0.z - ro.z) * fn.z) / (fn.x * rd.x + fn.y * rd.y + fn.z * rd.z);
        insetPos = ro + rd * t;

        float s0 = Vector3.Cross(p0 - insetPos, p1 - insetPos).magnitude * 0.5f;    //Triangle0 Size 
        float s1 = Vector3.Cross(p1 - insetPos, p2 - insetPos).magnitude * 0.5f;    //Triangle1 Size
        float s2 = Vector3.Cross(p2 - insetPos, p0 - insetPos).magnitude * 0.5f;    //Triangle2 Size 
        float sAll = fn.magnitude * 0.5f;   //TriangleAll Size


        bool isinsectInTri = Mathf.Abs(sAll - (s0 + s1 + s2)) < 0.001f;// is Intersecting point in Triangle

        return isinsectInTri;
    }

    public static bool PointInTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 insetPos)
    {
        Vector3 fn = Vector3.Cross(p1 - p0, p2 - p0).normalized;//faceNormal

        insetPos = Vector3.ProjectOnPlane(insetPos, fn);
        p0 = Vector3.ProjectOnPlane(p0, fn);
        p1 = Vector3.ProjectOnPlane(p1, fn);
        p2 = Vector3.ProjectOnPlane(p2, fn);

        float s0 = Vector3.Cross(p0 - insetPos, p1 - insetPos).magnitude * 0.5f;    //Triangle0 Size 
        float s1 = Vector3.Cross(p1 - insetPos, p2 - insetPos).magnitude * 0.5f;    //Triangle1 Size
        float s2 = Vector3.Cross(p2 - insetPos, p0 - insetPos).magnitude * 0.5f;    //Triangle2 Size 
        float sAll = Vector3.Cross(p1 - p0, p2 - p0).magnitude * 0.5f;   //TriangleAll Size

        return Mathf.Abs(sAll - (s0 + s1 + s2)) < 0.001f; // to remove small triangle I Increased threshold
    }

    //Debug
    public static void DrawATriangle(Vector3 p0, Vector3 p1, Vector3 p2, Color color)
    {
        Debug.DrawLine(p0, p1, color, 10);
        Debug.DrawLine(p1, p2, color, 10);
        Debug.DrawLine(p2, p0, color, 10);

    }
}
