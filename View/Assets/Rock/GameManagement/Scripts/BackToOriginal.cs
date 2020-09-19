using System.Collections;
using UnityEngine;

public class BackToOriginal : MonoBehaviour
{
    Vector3 targetPos;
    Quaternion targetRot;
    InputParent inputParent;
    readonly float duration = 0.69f;

    private void Start()
    {
        targetPos = transform.position;
        targetRot = transform.rotation;
        inputParent = GetComponent<InputParent>();
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

    private IEnumerator LerpBack()
    {
        if (inputParent != null)
        {
            inputParent.DisableController();
        }


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


        if (inputParent != null)
        {
            inputParent.EnableController();
        }
    }
}
