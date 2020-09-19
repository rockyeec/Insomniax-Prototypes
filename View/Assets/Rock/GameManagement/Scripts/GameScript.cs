using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static event System.Action OnPause = delegate { };
    public static event System.Action OnUnpause = delegate { };
    public static event System.Action OnGlassesOn = delegate { };
    public static event System.Action OnGlassesOff = delegate { };

    [SerializeField] private Button menuButton = null;
    [SerializeField] private Button unpauseButton = null;
    [SerializeField] private Button glassesButton = null;

    private CameraSpecialEffects cam;
    private bool isGlassesOn = false;

    private void Awake()
    {
        menuButton.onClick.AddListener(Pause);
        unpauseButton.onClick.AddListener(Unpause);
        glassesButton.onClick.AddListener(PutOnGlasses);

        cam = Camera.main.gameObject.AddComponent<CameraSpecialEffects>();

        StartCoroutine(WaitForTheRestToSetUp());
    }

    IEnumerator WaitForTheRestToSetUp()
    {
        yield return new WaitForEndOfFrame();

        Pause();
    }

    private void Pause()
    {
        OnPause();
        Time.timeScale = 0.0f;

        cam.ZoomOut();
    }

    private void Unpause()
    {
        OnUnpause();
        Time.timeScale = 1.0f;

        cam.ZoomIn();
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
