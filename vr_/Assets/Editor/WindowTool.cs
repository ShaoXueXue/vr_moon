using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using cakeslice;
public class WindowTool : EditorWindow
{
    public Object rootobj;

    public Object go;

    public bool isRender;

    [MenuItem("编辑器工具/显示工具窗口")]
    public static void ShowWindow()
    {
        WindowTool.CreateInstance<WindowTool>().Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("给层级下的所有含有meshRenderer的物体添加OutLine", EditorStyles.boldLabel);
        rootobj = EditorGUILayout.ObjectField("父级对象", this.rootobj, typeof(GameObject), true);
        isRender = GUILayout.Toggle(isRender, "要不要设置StaticEquipment层级");
        if (GUILayout.Button("添加"))
        {
            AddOutLine((GameObject)rootobj);
        }
    }

    private void AddOutLine(GameObject rootGo)
    {
        foreach (var item in rootGo.GetComponentsInChildren<MeshRenderer>())
        {
            if (item.enabled)
            {
                if (!item.GetComponent<Outline>())
                    item.gameObject.AddComponent<Outline>();
            }
            else
            {
                if (item.gameObject.GetComponent<Outline>())
                    DestroyImmediate(item.GetComponent<Outline>());
            }
            if (isRender)
            {
                if (item.gameObject.layer != 28)
                    item.gameObject.layer = 28;
            }

        }
    }
}
