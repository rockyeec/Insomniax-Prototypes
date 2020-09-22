using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameScript : MonoBehaviour
{
    private static GameScript instance;
    public static event Action OnPause = delegate { };
    public static event Action OnUnpause = delegate { };
    public static event Action OnGlassesOn = delegate { };
    public static event Action OnGlassesOff = delegate { };

    [SerializeField] private Button glassesButton = null;
    [SerializeField] private Button menuButton = null;

    private CameraSpecialEffects cam;
    private bool isGlassesOn = false;

    private void Awake()
    {
        instance = this;

        glassesButton.onClick.AddListener(PutOnGlasses);
        menuButton.onClick.AddListener(Pause);

        cam = Camera.main.gameObject.AddComponent<CameraSpecialEffects>();

        StartCoroutine(WaitForTheRestToSetUp());
        
    }

    IEnumerator WaitForTheRestToSetUp()
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

    private void PutOnGlasses()
    {
        isGlassesOn = !isGlassesOn;
        if (isGlassesOn)
        {
            OnGlassesOn();
        }
        else
        {
            OnGlassesOff();
        }
    }

}
