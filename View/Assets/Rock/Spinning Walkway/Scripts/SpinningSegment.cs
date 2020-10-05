﻿using UnityEngine;

public class SpinningSegment : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 6.9f;
    [SerializeField] private Vector3 spinAxis = Vector3.forward;

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

    private void GameScript_OnGlassesOn()
    {
        enabled = true;
    }
    private void GameScript_OnGlassesOff()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        transform.Rotate(spinAxis, Time.fixedDeltaTime * spinSpeed);
    }
}
