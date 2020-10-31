using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TempMenuHandler))]
public class DesignerToolForMenuHandler : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}

[CustomEditor(typeof(TempMenuPageHandler))]
public class DesignerToolForMenuPage : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TempMenuPageHandler menuPage = target as TempMenuPageHandler;

        if (GUILayout.Button("Initialize"))
        {
            menuPage.Init();
        }

        // temp
        EditorGUILayout.LabelField("Children", menuPage.transform.childCount.ToString());
    }
}



