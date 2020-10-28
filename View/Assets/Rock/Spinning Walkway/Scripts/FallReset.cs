﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput = null;
    bool isPuttingOnGlasses = false;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
    }

    private void GameScript_OnGlassesOn()
    {
        Invoke("ResetIsPuttingOnGlasses", CurveManager.AnimationDuration);
    }

    void ResetIsPuttingOnGlasses()
    {
        isPuttingOnGlasses = false;
    }

    void Update()
    {
        if (transform.position.y < -8.0f)
        {
            if (!isPuttingOnGlasses)
            {
                isPuttingOnGlasses = true;
                playerInput.PressGlassesButton();
            }
        }
    }
}