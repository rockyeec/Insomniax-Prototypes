using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSegmentArranger : MonoBehaviour
{
    [SerializeField] float segmentLength = 3.5f;
    
    readonly List<SegmentInfo> segments = new List<SegmentInfo>();
    DelayedWallLerper[] walls;
    float elapsed = 0.0f;
    readonly float duration = 0.69f;

    public static event System.Action OnStartSpinning = delegate { };
    public static event System.Action OnStopSpinning = delegate { };

    private void Start()
    {
        SpinningSegment[] tempSegments = GetComponentsInChildren<SpinningSegment>();
        int i = 0;
        foreach (var item in tempSegments)
        {
            SegmentInfo segment = new SegmentInfo
            {
                Segment = item,
                TargetPos = new Vector3(0.0f, 0.0f, 1.725f + i++ * segmentLength)
            };
            segments.Add(segment);

            item.transform.localPosition = Vector3.zero;
            item.GetComponent<BackToOriginalForSpinningPlatform>().SetTargetPosAndRot(transform.TransformPoint(Vector3.zero), transform.rotation);
        }

        walls = GetComponentsInChildren<DelayedWallLerper>();
        foreach (var wall in walls)
        {
            wall.Disable();
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
        foreach (var wall in walls)
        {
            wall.gameObject.SetActive(true);
        }
    }
    void EndLerp()
    {
        enabled = false;
        OnStartSpinning();
        foreach (var item in segments)
        {
            item.Segment.transform.localPosition = item.TargetPos;            
        }

        OnStopSpinning();
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
    }

}
