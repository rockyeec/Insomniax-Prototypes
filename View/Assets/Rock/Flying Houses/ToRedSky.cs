using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToRedSky : MonoBehaviour
{
    private void Start()
    {
        cam = Camera.main;
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
    [SerializeField] Color skyColor = Color.cyan;
    [SerializeField] Color redColor = Color.red;
    Color a, b;
    Camera cam;
    void StartLerp(in Color color)
    {
        enabled = true;
        elapsed = 0.0f;
        a = cam.backgroundColor;
        b = color;
    }
    void EndLerp()
    {
        enabled = false;
        cam.backgroundColor = b;
    }
    private void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        if (elapsed < CurveManager.AnimationDuration)
        {
            elapsed += Time.deltaTime;
            cam.backgroundColor = Color.LerpUnclamped(a, b, CurveManager.GlassesAnimT);
        }
        else 
        { 
            EndLerp(); 
        }
    }
}
