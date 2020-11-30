using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlurer : MonoBehaviour
{
    [SerializeField] Material blur = null;

    readonly static string key = "Vector1_14038EE1";
    readonly float blurrestValue = 0.015f;

    float elapsed = float.MaxValue;
    readonly float duration = 0.4f;

    float a, b;

    static CameraBlurer instance;
    private void Awake()
    {
        instance = this;

        Camera cam = Camera.main;

        transform.SetParent(cam.transform);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero.With(z: cam.nearClipPlane + 0.01f);
    }

    public static void Blur()
    {
        if (instance == null)
            return;
        instance.b = instance.blurrestValue;
        instance.StartLerp();
    }
    public static void Clear()
    {
        if (instance == null)
            return;
        instance.b = 0.0f;
        instance.StartLerp();
    }

    void StartLerp()
    {
        gameObject.SetActive(true);
        instance.a = instance.blur.GetFloat(key);
        enabled = true;
        elapsed = 0.0f;
    }
    void EndLerp()
    {
        enabled = false;
        blur.SetFloat(key, b);
        gameObject.SetActive(b == blurrestValue);
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = CurveManager.FadeCurve.Evaluate(elapsed / duration);
            float value = Mathf.LerpUnclamped(a, b, t);
            blur.SetFloat(key, value);
        }
        else
        {
            EndLerp();
        }
    }
}
