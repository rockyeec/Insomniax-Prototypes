using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class SpinningSegmentArranger : MonoBehaviour
{
    //[SerializeField] bool isArrange = false;
    [SerializeField] float segmentLength = 3.5f;
    
    List<SegmentInfo> segments = new List<SegmentInfo>();
    float elapsed = 0.0f;
    readonly float duration = 0.69f;

    public static event System.Action OnStartSpinning = delegate { };

    /*private void Update()
    {
        if (isArrange)
        {
            isArrange = false;

            SpinningSegment[] segments = GetComponentsInChildren<SpinningSegment>();
            if (segments != null)
            {
                if (segments.Length != 0)
                {
                    for (int i = 0; i < segments.Length; i++)
                    {
                        segments[i].transform.localPosition = new Vector3(0.0f, 0.0f, i * segmentLength);
                    }
                }
            }
        }
    }*/

    private void Start()
    {
        SpinningSegment[] tempSegments = GetComponentsInChildren<SpinningSegment>();
        int i = 0;
        foreach (var item in tempSegments)
        {
            SegmentInfo segment = new SegmentInfo();
            segment.Segment = item;
            segment.TargetPos = new Vector3(0.0f, 0.0f, i++ * segmentLength);
            segment.WallLerpers = item.GetComponentsInChildren<DelayedWallLerper>();
            segments.Add(segment);

            item.transform.localPosition = Vector3.zero;
        }

        GameScript.OnGlassesOn += StartLerp;

        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= StartLerp;
    }

    void StartLerp()
    {
        enabled = true;
        elapsed = 0.0f;
        foreach (var item in segments)
        {
            item.OriPos = item.Segment.transform.localPosition;
        }
    }
    void EndLerp()
    {
        enabled = false;
        OnStartSpinning();
        foreach (var item in segments)
        {
            item.Segment.transform.localPosition = item.TargetPos;
            foreach (var wall in item.WallLerpers)
            {
                wall.FadeOut();
            }
        }
    }
    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = CurveManager.Curve.Evaluate(elapsed / duration);

            foreach (var item in segments)
            {
                item.Segment.transform.localPosition = Vector3.LerpUnclamped(item.OriPos, item.TargetPos, t);
            }
        }
        else
        {
            EndLerp();
        }
    }

    public class SegmentInfo
    { 
        public SpinningSegment Segment { get; set; }
        public Vector3 TargetPos { get; set; }
        public Vector3 OriPos { get; set; }
        public DelayedWallLerper[] WallLerpers { get; set; }
    }

}
