using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpinningSegmentArranger))]
public class DesignerToolLevel2 : Editor
{
    public override void OnInspectorGUI()
    {
        SpinningSegmentArranger spinningSegmentArranger = (SpinningSegmentArranger)target;

        EditorGUILayout.HelpBox("Yo Aidan Check this out!", MessageType.Info);
        
        DrawDefaultInspector();        

        if (GUILayout.Button("Arrange Segments"))
        {
            spinningSegmentArranger.ArrangeSegments();
        }
        if (GUILayout.Button("Refresh Platforms"))
        {
            spinningSegmentArranger.RefreshAllPlatforms();
        }

        EditorGUILayout.Space(35.0f);
        EditorGUILayout.HelpBox("Destroys Built Platforms!", MessageType.Warning);
        if (GUILayout.Button("Generate Segments"))
        {
            spinningSegmentArranger.GenerateSegments();
        }
    }
}
