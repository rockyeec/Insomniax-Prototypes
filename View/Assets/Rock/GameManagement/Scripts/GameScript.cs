using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameScript : MonoBehaviour
{
    private static bool isStartGame = true;
    public static event Action OnPause = delegate { };
    public static event Action OnUnpause = delegate { };
    public static event Action OnGlassesOn = delegate { };
    public static event Action OnGlassesOff = delegate { };

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        Application.targetFrameRate = 60;


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
    }

    public static void Unpause()
    {
        if (Input.touchCount > 1)
            return;

        OnUnpause();
        Time.timeScale = 1.0f;
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
