using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TempMenuPageHandler))]
public class DesignerToolForMenu : Editor
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOpeion()
    {
        PlayerPrefs.DeleteAll();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
