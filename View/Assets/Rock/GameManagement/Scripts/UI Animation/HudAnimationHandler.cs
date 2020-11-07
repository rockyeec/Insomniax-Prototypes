using System.Collections;
using UnityEngine;

public class HudAnimationHandler : UISlidingAnimation
{
    protected override void Start()
    {
        base.Start();

        OriginalPosition = transform.position;

        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnPlay += GameScript_OnPlay;
    }
    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnPlay -= GameScript_OnPlay;
    }

    private void GameScript_OnPause()
    {
        SlideOut();
    }
    private void GameScript_OnPlay()
    {
        SlideIn();
    }
}
