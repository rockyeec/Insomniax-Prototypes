using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSegmentArranger : MonoBehaviour
{
    [SerializeField] Transform theDoor = null;

    [SerializeField] int segmentCount = 5;
    [SerializeField] float segmentLength = 3.5f;
    
    readonly List<SegmentInfo> segments = new List<SegmentInfo>();
    DelayedWallLerper[] walls;
    float elapsed = 0.0f;
    readonly float duration = 0.69f;

    public static event System.Action OnStartSpinning = delegate { };
    public static event System.Action OnStopSpinning = delegate { };

    public void GenerateSegments()
    {
        theDoor.SetParent(null);
        segments.Clear();
        SpinningSegment[] tempSegments = GetComponentsInChildren<SpinningSegment>();
        List<SpinningSegment> tempSegmentList = new List<SpinningSegment>();       
        tempSegmentList.AddRange(tempSegments);
        while (tempSegmentList.Count < segmentCount)
        {
            tempSegmentList.Add( Instantiate(tempSegments[tempSegments.Length - 1], transform));
        }
        while (tempSegmentList.Count > segmentCount)
        {
            SpinningSegment tempSeg = tempSegmentList[tempSegmentList.Count - 1];
            tempSegmentList.Remove(tempSeg);
            DestroyImmediate(tempSeg.gameObject);
        }
        theDoor.SetParent(tempSegmentList[tempSegmentList.Count - 1].Room);
        theDoor.localPosition = Vector3.zero.With(y: 1.75f);

        int i = 0;
        foreach (var item in tempSegmentList)
        {
            item.name = "Segment(" + (i + 1).ToString() + ")";
            SegmentInfo segment = new SegmentInfo
            {
                Segment = item,
                TargetPos = new Vector3(0.0f, 0.0f, 1.725f + i++ * segmentLength)
            };
            segments.Add(segment);

            item.transform.localPosition = segment.TargetPos;
            item.Room.localScale = Vector3.one.With(z: segmentLength / 10.0f);
        }
    }

    public void ArrangeSegments()
    {
        segments.Clear();
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

            item.transform.localPosition = segment.TargetPos;
            item.Room.localScale = Vector3.one.With(z: segmentLength / 10.0f);
        }
    }

    public void RefreshAllPlatforms()
    {
        PlatformHallway[] tempPlatforms = GetComponentsInChildren<PlatformHallway>();
        foreach (var item in tempPlatforms)
        {
            item.Refresh();
        }
    }

    private void Start()
    {
        ArrangeSegments();

        foreach (var item in segments)
        {
            item.Segment.transform.localPosition = Vector3.zero;
            item.Segment.GetComponent<BackToOriginalForSpinningPlatform>()
                .SetTargetPosAndRot(transform.TransformPoint(Vector3.zero), transform.rotation);
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
