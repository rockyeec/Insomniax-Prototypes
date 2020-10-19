using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    Vector3 top;
    Vector3 bottom;

    float elapsed = 0.0f;
    float duration;

    float depth = 1.5f;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;

        Instantiate(HouseFlyManager.HousePrefabs.GetRandom(), transform).transform.localPosition = Vector3.zero;

        top = transform.localPosition + Vector3.up * Random.Range(0.4f, 8.0f);
        bottom = transform.localPosition;

        ResetHouse();
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        ResetHouse();
        enabled = false;
    }

    private void GameScript_OnGlassesOn()
    {
        enabled = true;
    }

    private void ResetHouse()
    {
        if (bottom.y > top.y)
        {
            Vector3 swap = top;
            top = bottom;
            bottom = swap;
        }
        elapsed = 0.0f;

        duration = Random.Range(1.2f, 14.5f);
        depth = Random.Range(0.5f, 3.5f);
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.LerpUnclamped(
                bottom.With(z: transform.localPosition.z),
                top.With(z: transform.localPosition.z),
                CurveManager.Curve.Evaluate(elapsed / duration));
        }
        else
        {
            elapsed -= duration;
            Vector3 swap = top;
            top = bottom;
            bottom = swap;
        }

        if (transform.localPosition.z < depth)
            transform.localPosition += Vector3.forward * Time.deltaTime * 0.16f;
    }

}
