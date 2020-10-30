using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpinningSegmentArranger))]
public class DesignerToolLevel2 : Editor
{
    public override void OnInspectorGUI()
    {
        SpinningSegmentArranger spinningSegmentArranger = (SpinningSegmentArranger)target;
        
        DrawDefaultInspector();        

        if (GUILayout.Button("Rearrange Segments"))
        {
            spinningSegmentArranger.ArrangeSegments();
        }
        if (GUILayout.Button("Refresh Platforms"))
        {
            spinningSegmentArranger.RefreshAllPlatforms();
        }

        EditorGUILayout.Space(35.0f);
        EditorGUILayout.HelpBox("Destroys Built Platforms if new Segment Count is less than Previous Segment Count!", MessageType.Warning);
        if (GUILayout.Button("Generate Segments"))
        {
            spinningSegmentArranger.GenerateSegments();
        }
    }
}
