using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static GameScript instance;
    public static event System.Action OnPause = delegate { };
    public static event System.Action OnUnpause = delegate { };
    public static event System.Action OnGlassesOn = delegate { };
    public static event System.Action OnGlassesOff = delegate { };

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
        OnPause();
        Time.timeScale = 0.0f;

        instance.cam.ZoomOut();
    }

    public static void Unpause()
    {
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
