using System.Collections;
using UnityEngine;

public class UISlidingAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 outwardsDirection = Vector3.zero;
    [SerializeField] private float percentageOvershoot = 0.5f;
    private AnimationCurve curve = null;

    public float PercentageOvershoot 
    { 
        get { return percentageOvershoot; } 
        set { percentageOvershoot = value; } 
    }

    readonly private float duration = 0.69f;
    private Vector3 originalPos;
    private Vector3 offScreenPos;
    private Vector3 overshootPos;

    protected virtual void Start()
    {
        originalPos = transform.position;
        offScreenPos = originalPos + outwardsDirection * 1337.0f;
        overshootPos = originalPos - outwardsDirection * 133.7f;

        curve = CurveManager.Curve;
    }

    public void SlideIn()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Slide(originalPos, true, percentageOvershoot));
    }
    public void SlideOut()
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Slide(offScreenPos, false, 1 - percentageOvershoot));
    }

    public void SnapOut()
    {
        transform.position = offScreenPos;
        gameObject.SetActive(false);
    }
    public void SnapIn()
    {
        transform.position = originalPos;
    }

    private IEnumerator Slide(Vector3 target, bool isActive, float phase1Duration = 0.5f)
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

            t = curve.Evaluate(t);

            transform.position =
                isPhase1
                ? Vector3.Lerp(ori, overshootPos, t)
                : Vector3.Lerp(overshootPos, target, t);

            yield return null;
        }
        transform.position = target;
        gameObject.SetActive(isActive);
    }
}
