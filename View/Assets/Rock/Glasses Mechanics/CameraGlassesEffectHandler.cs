using System.Collections;
using UnityEngine;

public class CameraGlassesEffectHandler : MonoBehaviour
{
    Camera cam = null;
    [SerializeField] Transform camParent = null;


    float targetFov;
    Vector3 targetPos;
    float elapsed = float.MaxValue;
    float duration;
    float startFov;
    Vector3 startPos;



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

        StartLerp(60.0f, Vector3.zero);
    }

    private void GameScript_OnGlassesOn()
    {
        FillUpVarIfNull();

        StartLerp(80.0f, Vector3.forward * 1.0f);
    }

    private void FillUpVarIfNull()
    {
        if (cam == null)
            cam = Camera.main;
    }



    private void Start()
    {
        duration = CurveManager.AnimationDuration;
        enabled = false;
    }
    private void StartLerp(in float targetFov, Vector3 targetPos)
    {
        FillUpVarIfNull();
        enabled = true;

        elapsed = 0.0f;
        duration = CurveManager.AnimationDuration;

        startFov = cam.fieldOfView; 
        startPos = camParent.localPosition;

        this.targetFov = targetFov;
        this.targetPos = targetPos;
    }
    private void EndLerp()
    {
        FillUpVarIfNull();
        enabled = false;

        camParent.localPosition = targetPos;
        cam.fieldOfView = targetFov;
    }
    private void Update()
    {
        if (elapsed < duration)
        {
            FillUpVarIfNull();
            elapsed += Time.deltaTime;

            float t = CurveManager.FadeCurve.Evaluate(elapsed / duration);

            camParent.localPosition = Vector3.LerpUnclamped(startPos, targetPos, t);
            cam.fieldOfView = Mathf.LerpUnclamped(startFov, targetFov, t);
        }
        else
        {
            EndLerp();
        }
    }
}
