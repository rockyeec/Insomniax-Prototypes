using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyBlinker : MonoBehaviour
{
    Image rend;
    Color a, b;
    float elapsed = 0.0f;
    private void Awake()
    {
        rend = GetComponent<Image>();
        a = rend.color.WithAlpha(0.0f);
        b = rend.color.WithAlpha(1.0f);
    }

    private void Update()
    {
        float duration = 0.4f;
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = CurveManager.FadeCurve.Evaluate(elapsed / duration);
            rend.color = Color.LerpUnclamped(a, b, t);
        }
        else
        {
            elapsed -= duration;
            rend.color = b;
            Swap();
        }
    }

    void Swap()
    {
        Color c = a;
        a = b;
        b = c;
    }
}
