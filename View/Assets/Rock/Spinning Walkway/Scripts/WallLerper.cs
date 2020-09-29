using System.Collections;
using UnityEngine;

public class WallLerper : MonoBehaviour
{
    private MeshRenderer ren = null;
    private Collider col = null;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;

        col = GetComponent<Collider>();
        ren = GetComponent<MeshRenderer>();
        ren.materials = new Material[0];
        ren.material = MaterialManager.WallMaterial;
    }

    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        StopAllCoroutines();
        StartCoroutine(LerpMaterial(MaterialManager.FadedOutMaterial, MaterialManager.WallMaterial, true));
    }

    private void GameScript_OnGlassesOn()
    {
        StopAllCoroutines();
        StartCoroutine(LerpMaterial(MaterialManager.WallMaterial, MaterialManager.FadedOutMaterial, false));
    }

    private IEnumerator LerpMaterial(Material cur, Material target, bool isColliderActive)
    {
        float elapsed = 0.0f;
        float duration = CurveManager.AnimationDuration;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            t = CurveManager.Curve.Evaluate(t);

            ren.material.Lerp(cur, target, t);

            yield return null;
        }

        ren.material = target;
        col.enabled = isColliderActive;
    }
}
