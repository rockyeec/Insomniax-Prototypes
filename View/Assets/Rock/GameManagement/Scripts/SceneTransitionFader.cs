﻿using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SceneTransitionFader : MonoBehaviour
{
    private static SceneTransitionFader instance;
    Image image;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        image = GetComponentInChildren<Image>();
        image.color = image.color.WithAlpha(1.0f);
        FadeOut();
    }


    public static void FadeIn()
    {
        instance.gameObject.SetActive(true);
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.Fade(1.0f, 0.69f));
    }
    public static void FadeOut()
    {
        instance.gameObject.SetActive(true);
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.Fade(0.0f, 6.9f));
    }

    IEnumerator Fade(float targetAlpha, float duration)
    {
        while (!SplashScreen.isFinished)
        {
            yield return null;
        }

        float elapsed = 0.0f;

        Color original = image.color;
        Color target = image.color.WithAlpha(targetAlpha);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = CurveManager.FadeCurve.Evaluate( elapsed / duration);

            image.color = Color.Lerp(original, target, t);

            yield return null;
        }

        image.color = target;
        gameObject.SetActive(targetAlpha == 1.0f);
    }
}