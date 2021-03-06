﻿using System.Collections;
using UnityEngine;

public class UISlidingAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 outwardsDirection = Vector3.zero;
    [SerializeField] private float percentageOvershoot = 0.5f;
    public Vector3 OutwardsDirection
    {
        get { return outwardsDirection; }
        set { outwardsDirection = value; }
    }
    public float PercentageOvershoot 
    { 
        get { return percentageOvershoot; } 
        set { percentageOvershoot = value; } 
    }
    public Vector3 OriginalPosition 
    { 
        set 
        { 
            originalPos = value; 
            offScreenPos = originalPos + outwardsDirection * 1337.0f;
            overshootPos = originalPos - outwardsDirection * 133.7f;
        } 
    }

    private Vector3 originalPos;
    private Vector3 offScreenPos;
    private Vector3 overshootPos;


    float elapsed = float.MaxValue;
    float duration = 0.0f;
    float phase1;
    float phase2;
    bool isActive;

    Vector3 ori;
    Vector3 target;

    protected virtual void Start()
    {
        enabled = false;
    }

    public void SlideIn()
    {
        gameObject.SetActive(true);
        StartLerp(originalPos, true, percentageOvershoot);
    }
    public void SlideOut()
    {
        gameObject.SetActive(true);
        StartLerp(offScreenPos, false, 1 - percentageOvershoot);
    }

    public void SnapOut()
    {
        enabled = false;
        transform.position = offScreenPos;
        gameObject.SetActive(false);
    }
    public void SnapIn()
    {
        enabled = false;
        gameObject.SetActive(true);
        transform.position = originalPos;
    }

    


    private void StartLerp(Vector3 target, bool isActive, float phase1Duration = 0.5f)
    {
        if (duration == 0.0f)
        {
            duration = CurveManager.AnimationDuration;
        }

        elapsed = 0.0f;
        phase1 = duration * phase1Duration;
        phase2 = duration - phase1;
        ori = transform.position;

        this.target = target;
        this.isActive = isActive;

        enabled = true;
    }

    private void EndLerp()
    {
        transform.position = target;
        gameObject.SetActive(isActive);

        enabled = false;
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            bool isPhase1 = elapsed < phase1;

            float t = 
                isPhase1
                ? elapsed / phase1
                : (elapsed - phase1) / phase2;

            t = CurveManager.Curve.Evaluate(t);

            transform.position =
                isPhase1
                ? Vector3.LerpUnclamped(ori, overshootPos, t)
                : Vector3.LerpUnclamped(overshootPos, target, t);
        }
        else
        {
            EndLerp();
        }
    }
}
