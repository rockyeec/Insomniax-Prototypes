using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRedLight : MonoBehaviour
{
    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        StartLerp(skyColor);
    }

    private void GameScript_OnGlassesOn()
    {
        StartLerp(redColor);
    }


    float elapsed = 0.0f;
    [SerializeField] Light thisLight = null;
    [SerializeField] Color skyColor = Color.cyan;
    [SerializeField] Color redColor = Color.red;
    Color a, b;
    void StartLerp(in Color color)
    {
        enabled = true;
        elapsed = 0.0f;
        a = thisLight.color;
        b = color;
    }
    void EndLerp()
    {
        enabled = false;
        thisLight.color = b;
    }
    private void Update()
    {
        if (elapsed < CurveManager.AnimationDuration)
        {
            elapsed += Time.deltaTime;
            thisLight.color = Color.LerpUnclamped(a, b, CurveManager.GlassesAnimT);
        }
        else
        {
            EndLerp();
        }
    }
}
