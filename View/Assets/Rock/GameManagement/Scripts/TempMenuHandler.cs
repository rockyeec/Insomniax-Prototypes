using UnityEngine;

public class TempMenuHandler : MonoBehaviour
{
    [SerializeField] private MenuPage pauseMenu = null;
    [SerializeField] private MenuPage settings = null;

    private void Start()
    {
        pauseMenu.Init();
        settings.Init();

        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;

        pauseMenu.SnapOut();
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

    [System.Serializable]
    public class MenuPage
    {
        [SerializeField] private Transform menuPageParent = null;
        private UISlidingAnimation[] animatedChildren = null;

        public MenuPage()
        {
            menuPageParent = null;
            animatedChildren = null;
        }

        public void Init()
        {
            animatedChildren = menuPageParent.GetComponentsInChildren<UISlidingAnimation>();

            float range = 0.2f;
            int length = animatedChildren.Length;
            for (int i = 0; i < length; i++)
            {
                animatedChildren[i].PercentageOvershoot
                    = 0.8f - (i * range / length);
            }
        }
        public void SlideIn()
        {
            foreach (var item in animatedChildren)
            {
                item.SlideIn();
            }
        }
        public void SlideOut()
        {
            foreach (var item in animatedChildren)
            {
                item.SlideOut();
            }
        }
        public void SnapIn()
        {
            foreach (var item in animatedChildren)
            {
                item.SnapIn();
            }
        }
        public void SnapOut()
        {
            foreach (var item in animatedChildren)
            {
                item.SnapOut();
            }
        }
    }
}
