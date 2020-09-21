using System.Collections;
using UnityEngine;

public class UISlidingAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 outwardsDirection = Vector3.zero;
    [SerializeField] private bool isSlideInWhenPaused = false;
    [SerializeField] private float percentageOvershoot = 0.5f;

    readonly private float duration = 0.69f;
    private Vector3 originalPos;
    private Vector3 offScreenPos;
    private Vector3 overshootPos;


    private void Start()
    {
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;

        originalPos = transform.position;
        offScreenPos = originalPos + outwardsDirection * 1337.0f;
        overshootPos = originalPos - outwardsDirection * 133.7f;

        if (isSlideInWhenPaused)
        {
            Vector3 swap = originalPos;
            originalPos = offScreenPos;
            offScreenPos = swap;
        }
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
    }

    private void GameScript_OnPause()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(offScreenPos, 1 - percentageOvershoot));
    }
    private void GameScript_OnUnpause()
    {
        StopAllCoroutines();
        StartCoroutine(Slide(originalPos, percentageOvershoot));
    }

    private IEnumerator Slide(Vector3 target, float phase1Duration = 0.5f)
    {
        float elapsed = 0.0f;

        float phase1 = duration * phase1Duration;
        float phase2 = duration - phase1;

        Vector3 ori = transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            bool isPhase1 = elapsed < phase1;

            float t = 
                isPhase1
                ? elapsed / phase1
                : (elapsed - phase1) / phase2;
            t *= t;

            transform.position =
                isPhase1
                ? Vector3.Lerp(ori, overshootPos, t)
                : Vector3.Lerp(overshootPos, target, t);

            yield return null;
        }
        transform.position = target;
    }
}
