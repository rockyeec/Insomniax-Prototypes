using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public static event System.Action OnPause = delegate { };
    public static event System.Action OnUnpause = delegate { };

    [SerializeField] private Button menuButton = null;
    [SerializeField] private Button unpauseButton = null;
    [SerializeField] private Button glassesButton = null;

    private CameraSpecialEffects cam;

    private void Awake()
    {
        menuButton.onClick.AddListener(Pause);
        unpauseButton.onClick.AddListener(Unpause);

        SetButtonVisibilityIfPauseIs(false);

        cam = Camera.main.gameObject.AddComponent<CameraSpecialEffects>();
    }

    private void Pause()
    {
        OnPause();
        Time.timeScale = 0.0f;
        SetButtonVisibilityIfPauseIs(true);

        cam.ZoomOut();
    }

    private void Unpause()
    {
        OnUnpause();
        Time.timeScale = 1.0f;
        SetButtonVisibilityIfPauseIs(false);

        cam.ZoomIn();
    }

    private void SetButtonVisibilityIfPauseIs(bool isPause)
    {
        menuButton.gameObject.SetActive(!isPause);
        glassesButton.gameObject.SetActive(!isPause);
        unpauseButton.gameObject.SetActive(isPause);
    }

}
