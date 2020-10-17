using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpinningSegmentArranger : MonoBehaviour
{
    [SerializeField] bool isArrange = false;
    [SerializeField] float segmentLength = 3.5f;

    private void Update()
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
    }
}
