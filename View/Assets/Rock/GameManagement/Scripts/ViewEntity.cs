using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewEntity : MonoBehaviour
{
    private bool isPause;

    private void Awake()
    {
        GameScript.OnPause += GameScript_OnPause;
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
    }

    private void GameScript_OnPause(bool value)
    {
        isPause = value;
    }

    private void Update()
    {
        if (isPause) return;
        ViewUpdate();
    }
    private void FixedUpdate()
    {
        if (isPause) return;
        ViewFixedUpdate();
    }
    private void LateUpdate()
    {
        if (isPause) return;
        ViewLateUpdate();
    }

    protected abstract void ViewUpdate();
    protected abstract void ViewFixedUpdate();
    protected abstract void ViewLateUpdate();
}
