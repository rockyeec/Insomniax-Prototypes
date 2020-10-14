using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSkyBoxScript : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;
    }
    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
    }

    private void GameScript_OnUnpause()
    {
        if (cam == null)
            cam = Camera.main;

        cam.clearFlags = CameraClearFlags.Skybox;
    }

    private void GameScript_OnPause()
    {
        if (cam == null)
            cam = Camera.main;

        cam.clearFlags = CameraClearFlags.SolidColor;
    }
}
