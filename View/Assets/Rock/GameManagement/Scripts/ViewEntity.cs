using UnityEngine;

public abstract class ViewEntity : MonoBehaviour
{
    private bool isPause;
    protected readonly float lerpRate = 3.3f;



    private void Start()
    {
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }



    private void GameScript_OnPause()
    {
        isPause = true;
    }

    private void GameScript_OnUnpause()
    {
        isPause = false;
    }

    private void GameScript_OnGlassesOff()
    {
        OnGlassesOff();
    }

    private void GameScript_OnGlassesOn()
    {
        OnGlassesOn();
    }




    private void Update()
    {
        if (isPause) return;
        UpdateEntity();
    }
    private void FixedUpdate()
    {
        if (isPause) return;
        FixedUpdateEntity();
    }
    private void LateUpdate()
    {
        if (isPause) return;
        LateUpdateEntity();
    }

    protected virtual void UpdateEntity() { }
    protected virtual void FixedUpdateEntity() { }
    protected virtual void LateUpdateEntity() { }


    protected virtual void OnGlassesOn() { }
    protected virtual void OnGlassesOff() { }
}
