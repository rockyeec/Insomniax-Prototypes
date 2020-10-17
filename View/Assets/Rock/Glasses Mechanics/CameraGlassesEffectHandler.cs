using System.Collections;
using UnityEngine;

public class CameraGlassesEffectHandler : MonoBehaviour
{
    Camera cam = null;
    Transform camParent = null;
    private void Awake()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn; 
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        FillUpVarIfNull();

        StopAllCoroutines();
        StartCoroutine(Transition(60.0f, Vector3.zero));
    }

    private void GameScript_OnGlassesOn()
    {
        FillUpVarIfNull();

        StopAllCoroutines();
        StartCoroutine(Transition(85.0f, Vector3.forward * 1.5f));
    }

    private void FillUpVarIfNull()
    {
        cam = cam ?? Camera.main;
        camParent = cam.transform.parent;
    }

    IEnumerator Transition(float targetFov, Vector3 targetPos)
    {
        float elapsed = 0.0f;
        float duration = CurveManager.AnimationDuration;

        float startFov = cam.fieldOfView;
        Vector3 startPos = camParent.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = CurveManager.FadeCurve.Evaluate(elapsed / duration);

            camParent.localPosition = Vector3.LerpUnclamped(startPos, targetPos, t);
            cam.fieldOfView = Mathf.LerpUnclamped(startFov, targetFov, t);

            yield return null;
        }

        camParent.localPosition = targetPos;
        cam.fieldOfView = targetFov;
    }
}
