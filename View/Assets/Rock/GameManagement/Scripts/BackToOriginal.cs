using UnityEngine;

public class BackToOriginal : MonoBehaviour
{
    private Vector3 oriPos;
    private Quaternion oriRot;
    private Vector3 targetPos;
    private Quaternion targetRot;

    private float elapsed = float.MaxValue;

    virtual protected void Start()
    {
        targetPos = transform.position;
        targetRot = transform.rotation;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        OnStartLerp();        
    }

    virtual protected void OnStartLerp() 
    {
        elapsed = 0.0f;
        oriPos = transform.position;
        oriRot = transform.rotation;
        enabled = true;
    }
    virtual protected void OnEndLerp() 
    {
        transform.position = targetPos;
        transform.rotation = targetRot;
        enabled = false;
    }

    private void FixedUpdate()
    {
        if (elapsed < CurveManager.AnimationDuration)
        {
            elapsed += Time.fixedDeltaTime;

            float t = elapsed / CurveManager.AnimationDuration;
            t = CurveManager.Curve.Evaluate(t);

            transform.position = Vector3.LerpUnclamped(oriPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(oriRot, targetRot, t);
        }
        else
        {
            OnEndLerp();
        }
    }
}
