using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevleManager))]
public class LevleManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevleManager levleManager = (LevleManager)target;
        if (GUILayout.Button("切换运行场景"))
        {
            levleManager.ChangeSceneByEditor();
        }
    }
}

//[CustomEditor(typeof(ParticleSystemTimeControl))]
//public class PaticleEditor : Editor
//{

//}
