using System.Collections;
using UnityEngine;
using System;

public class GameScript : MonoBehaviour
{
    private static GameScript instance;

    public static event Action OnPause = delegate { };
    public static event Action OnUnpause = delegate { };
    public static event Action OnPlay = delegate { };
    public static event Action OnGlassesOn = delegate { };
    public static event Action OnGlassesOff = delegate { };

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        instance = this;

        OnGlassesOn += GameScript_OnGlassesOn; 
        OnGlassesOff += GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        AudioManager.instance.PlayBgm("Main Music Normal");
    }

    private void GameScript_OnGlassesOn()
    {
        AudioManager.instance.PlayBgm("Main Music Glasses");
    }

    public static void Pause()
    {
        if (Input.touchCount > 1)
            return;

        OnPause();
        instance.StopAllCoroutines();
        Time.timeScale = 0.0f;
    }

    public static void Unpause()
    {
        if (Input.touchCount > 1)
            return;

        OnUnpause();
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.WaitForCameraEffects());
    }

    public static void PutOnGlasses()
    {
        OnGlassesOn();
    }
    public static void TakeOffGlasses()
    {
        OnGlassesOff();
    }

    IEnumerator WaitForCameraEffects()
    {
        yield return new WaitForSecondsRealtime(CurveManager.CameraAnimationDuration);
        OnPlay();
        Time.timeScale = 1.0f;
    }
}
