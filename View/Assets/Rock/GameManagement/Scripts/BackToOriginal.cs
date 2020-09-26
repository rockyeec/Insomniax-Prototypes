﻿using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BackToOriginal : MonoBehaviour
{
    private AnimationCurve curve = null;
    private Vector3 oriPos;
    private Quaternion oriRot;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private readonly float duration = 0.69f;

    private float elapsed = 0.69f;

    virtual protected void Start()
    {
        curve = CurveManager.Curve;

        targetPos = transform.position;
        targetRot = transform.rotation;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        OnStartLerp();        
    }

    virtual protected void OnStartLerp() 
    {
        elapsed = 0.0f;
        oriPos = transform.position;
        oriRot = transform.rotation;
        enabled = true;
    }
    virtual protected void OnEndLerp() 
    {
        transform.position = targetPos;
        transform.rotation = targetRot;
        enabled = false;
    }

    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = elapsed / duration;
            t = curve.Evaluate(t);

            transform.position = Vector3.Lerp(oriPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(oriRot, targetRot, t);
        }
        else
        {
            OnEndLerp();
        }
    }
}
