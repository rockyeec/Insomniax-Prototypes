using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOriginalScale : MonoBehaviour
{
    private Vector3 oriScale;
    private Vector3 targetScale;

    private float elapsed = float.MaxValue;


    virtual protected void Start()
    {
        targetScale = transform.localScale;
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
        oriScale = transform.localScale;
        enabled = true;
    }
    virtual protected void OnEndLerp()
    {
        transform.localScale = targetScale;
        enabled = false;
    }

    private void FixedUpdate()
    {
        if (elapsed < CurveManager.AnimationDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = elapsed / CurveManager.AnimationDuration;
            t = CurveManager.Curve.Evaluate(t);

            transform.localScale = Vector3.LerpUnclamped(oriScale, targetScale, t);
        }
        else
        {
            OnEndLerp();
        }
    }
}