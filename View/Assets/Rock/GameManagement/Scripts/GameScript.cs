using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameScript : MonoBehaviour
{
    private static GameScript instance;
    private static bool isStartGame = true;
    public static event Action OnPause = delegate { };
    public static event Action OnUnpause = delegate { };
    public static event Action OnGlassesOn = delegate { };
    public static event Action OnGlassesOff = delegate { };

    private CameraSpecialEffects cam;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        instance = this;

        cam = Camera.main.gameObject.AddComponent<CameraSpecialEffects>();

        if (isStartGame)
        {
            isStartGame = false;
            StartCoroutine(WaitAndPause());
        }
        
    }

    IEnumerator WaitAndPause()
    {
        yield return new WaitForEndOfFrame();

        Pause();
    }

    public static void Pause()
    {
        if (Input.touchCount > 1)
            return;

        OnPause();
        Time.timeScale = 0.0f;

        instance.cam.ZoomOut();
    }

    public static void Unpause()
    {
        if (Input.touchCount > 1)
            return;

        OnUnpause();
        Time.timeScale = 1.0f;

        instance.cam.ZoomIn();
    }

    public static void PutOnGlasses()
    {
        OnGlassesOn();
    }
    public static void TakeOffGlasses()
    {
        OnGlassesOff();
    }
}
