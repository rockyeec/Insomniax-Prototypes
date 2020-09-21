using System.Collections;
using UnityEngine;

public class BackToOriginal : MonoBehaviour
{
    Vector3 targetPos;
    Quaternion targetRot;
    readonly float duration = 0.69f;

    virtual protected void Start()
    {
        targetPos = transform.position;
        targetRot = transform.rotation;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        StartCoroutine(LerpBack());
    }

    virtual protected void OnStartLerp() { }
    virtual protected void OnEndLerp() { }

    private IEnumerator LerpBack()
    {
        OnStartLerp();

        float elapsed = 0.0f;
        Vector3 oriPos = transform.position;
        Quaternion oriRot = transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            transform.position = Vector3.Lerp(oriPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(oriRot, targetRot, t);

            yield return null;
        }
        transform.position = targetPos;
        transform.rotation = targetRot;
        OnEndLerp();
    }
}
