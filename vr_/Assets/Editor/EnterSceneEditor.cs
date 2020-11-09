using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnterSceneEffect))]
public class EnterSceneEditor : Editor
{
    private SerializedProperty CiemachineVirtualCameraArray;
    private SerializedProperty animationCamerasArray;
    private SerializedProperty defaultWaitTime;

    EnterSceneEffect script
    {
        get { return target as EnterSceneEffect; }
    }
    private void OnEnable()
    {
        CiemachineVirtualCameraArray = serializedObject.FindProperty("CiemachineVirtualCamera");
        animationCamerasArray = serializedObject.FindProperty("animationCameras");
        defaultWaitTime = serializedObject.FindProperty("defaultWaitTime");
    }
    public override void OnInspectorGUI()
    {
       
        script.mClip = (AudioClip)EditorGUILayout.ObjectField("入场音效", script.mClip, typeof(AudioClip),true);
        script.animationType = (EnterSceneEffect.EnterSceneAnimatorType)EditorGUILayout.EnumPopup("开场动画类型", script.animationType);
        serializedObject.Update();
        if (script.animationType == EnterSceneEffect.EnterSceneAnimatorType.ByDefault)
        {
            //script.CiemachineVirtualCameraWaitTime = EditorGUILayout.DelayedFloatField("调用此动画的等待时间", script.CiemachineVirtualCameraWaitTime);
            EditorGUILayout.PropertyField(defaultWaitTime, true);
        }
        else if (script.animationType == EnterSceneEffect.EnterSceneAnimatorType.ByCiemachineVirtual)
        {
            script.defaultWaitTime = EditorGUILayout.DelayedFloatField("调用此动画的等待时间", script.defaultWaitTime);
            EditorGUILayout.PropertyField(CiemachineVirtualCameraArray, true);
        }
        else if (script.animationType == EnterSceneEffect.EnterSceneAnimatorType.ByCinemachineFreeLook)
        {
            script.mYAxisValue = EditorGUILayout.DelayedFloatField("当前CinemachineFreeLook的m_YAxis的值", script.mYAxisValue);
            script.waitTime = EditorGUILayout.DelayedFloatField("调用此动画的等待时间", script.waitTime);
            EditorGUILayout.PropertyField(animationCamerasArray, true);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
