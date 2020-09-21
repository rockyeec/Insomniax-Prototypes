public class HudAnimationHandler : UISlidingAnimation
{
    protected override void Start()
    {
        base.Start();

        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;
    }
    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
    }

    private void GameScript_OnPause()
    {
        SlideOut();
    }
    private void GameScript_OnUnpause()
    {
        SlideIn();
    }

}
