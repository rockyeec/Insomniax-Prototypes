using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveManagerForFixedUpdate : MonoBehaviour
{
    public static float T { get; private set; }

    private float elapsed = float.MaxValue;
    private float duration;
    private void Start()
    {
        GameScript.OnGlassesOff += StartLerp;
        duration = CurveManager.AnimationDuration;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= StartLerp;
    }
    private void StartLerp()
    {
        enabled = true;
        elapsed = 0.0f;
        T = 0.0f;
    }
    private void EndLerp()
    {
        enabled = false;
        T = 1.0f;
    }
    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            T = CurveManager.Curve.Evaluate(elapsed / duration);
        }
        else
        {
            EndLerp();
        }
    }
}
