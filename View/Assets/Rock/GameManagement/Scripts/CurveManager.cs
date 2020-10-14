﻿using UnityEngine;

public class CurveManager : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve = null;
    [SerializeField] private AnimationCurve fadeCurve = null;
    [SerializeField] private float animationDuration = 0.69f;

    private static CurveManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static AnimationCurve Curve { get { return instance.curve; } }
    public static AnimationCurve FadeCurve { get { return instance.fadeCurve; } }
    public static float AnimationDuration { get { return instance.animationDuration; } }
}
