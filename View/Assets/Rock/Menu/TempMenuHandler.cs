using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempMenuHandler : MonoBehaviour
{
    [SerializeField] private Button menuButton = null;
    [SerializeField] private TempMenuPageHandler pauseMenu = null;
    [SerializeField] private TempMenuPageHandler settings = null;

    static bool isFirstLevel = true;

    private void Start()
    {
        menuButton.onClick.AddListener(GameScript.Pause);


        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;

        StartCoroutine(SnapToPosition());
    }

    IEnumerator SnapToPosition()
    {
        yield return new WaitForEndOfFrame();
        if (isFirstLevel)
        {
            isFirstLevel = false;
            pauseMenu.SnapIn();
        }
        else
        {
            pauseMenu.SnapOut();
        }
        settings.SnapOut();
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
    }

    private void GameScript_OnPause()
    {
        pauseMenu.SlideIn();
    }
    private void GameScript_OnUnpause()
    {
        pauseMenu.SlideOut();
        settings.SnapOut();
    }
    public void OnSettingsPress()
    {
        if (Input.touchCount > 1)
            return;
        pauseMenu.SlideOut();
        settings.SlideIn();
    }
    public void OnReturnPress()
    {
        if (Input.touchCount > 1)
            return;
        pauseMenu.SlideIn();
        settings.SlideOut();
    }

    public void OnExitPress()
    {
        if (Input.touchCount > 1)
            return;
        Application.Quit();
    }
}
