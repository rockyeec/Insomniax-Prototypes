using System.Collections;
using UnityEngine;

public class DelayedWallLerper : MonoBehaviour
{
    private MeshRenderer ren = null;
    private Collider col = null;

    protected virtual Material SolidMaterial { get { return MaterialManager.WallMaterial; } }
    protected virtual Material TransluscentMaterial { get { return MaterialManager.FadedOutMaterial; } }

    private void Start()
    {
        col = GetComponentInChildren<Collider>();
        ren = GetComponentInChildren<MeshRenderer>();
        ren.materials = new Material[0];
        ren.material = SolidMaterial;

        GameScript.OnGlassesOff += FadeIn;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= FadeIn;
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(LerpMaterial(TransluscentMaterial, SolidMaterial, true));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(LerpMaterial(SolidMaterial, TransluscentMaterial, false));
    }

    private IEnumerator LerpMaterial(Material cur, Material target, bool isColliderActive)
    {
        float elapsed = 0.0f;
        float duration = CurveManager.AnimationDuration;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            t = CurveManager.FadeCurve.Evaluate(t);

            ren.material.Lerp(cur, target, t);

            yield return null;
        }

        ren.material = target;
        if (col != null)
            col.enabled = isColliderActive;
    }
}
