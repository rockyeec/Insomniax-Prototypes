using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public void SlideIn()
    {
        StartLerp(inPos);
    }
    public void SlideOut()
    {
        StartLerp(outPos);
    }
    public void SnapIn()
    {
        b = inPos;
        transform.localPosition = inPos;
    }
    public void SnapOut()
    {
        b = outPos;
        transform.localPosition = outPos;
    }


    float elapsed = float.MaxValue;
    float duration = 0.0f;

    [SerializeField] Vector3 outwardDisplacement = new Vector3(0.0f, -420.0f, 0.0f);
    [SerializeField] bool isDefaultSnappedIn = false;

    Vector3 outPos, inPos;
    Vector3 a, b;
    private void Awake()
    {
        inPos = transform.localPosition;
        outPos = transform.localPosition + outwardDisplacement;

        if (isDefaultSnappedIn)
        {
            SnapIn();
        }
        else
        {
            SnapOut();
        }
    }
    private void StartLerp(Vector3 target)
    {
        enabled = true;
        elapsed = 0.0f;
        duration = CurveManager.AnimationDuration;

        a = transform.localPosition;
        b = target;
    }
    private void EndLerp()
    {
        enabled = false;
        transform.localPosition = b;
    }
    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = CurveManager.Curve.Evaluate(elapsed / duration);

            transform.localPosition = Vector3.LerpUnclamped(a, b, t);
        }
        else
        {
            EndLerp();
        }
    }
}
