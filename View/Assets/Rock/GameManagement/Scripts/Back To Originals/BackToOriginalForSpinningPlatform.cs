using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOriginalForSpinningPlatform : BackToOriginal
{
    [SerializeField] GameObject platform = null;
    override protected void Start()
    {
        platform.SetActive(false);

        GameScript.OnGlassesOn += GameScript_OnGlassesOn;

        base.Start();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
    }

    private void GameScript_OnGlassesOn()
    {
        platform.SetActive(true);
    }
    protected override void OnStartLerp()
    {
        base.OnStartLerp();
        platform.transform.localRotation = Quaternion.identity;
        platform.SetActive(false);
    }
}
