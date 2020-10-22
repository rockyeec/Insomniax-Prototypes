using System.Collections;
using UnityEngine;

public class DelayedWallLerper : MonoBehaviour
{
    [SerializeField] bool keepActive = false;
    private MeshRenderer ren = null;
    private Collider col = null;

    protected virtual Material SolidMaterial { get { return MaterialManager.WallMaterial; } }

    Color solid;
    Color faded;

    float elapsed = 0.0f;
    float duration;
    Color a;
    Color b;
    bool isColliderActive = false;

    private void Start()
    {
        col = GetComponentInChildren<Collider>();
        ren = GetComponentInChildren<MeshRenderer>();
        ren.materials = new Material[0];
        ren.material = SolidMaterial;

        solid = SolidMaterial.color;
        faded = SolidMaterial.color.WithAlpha(0.0f);

        GameScript.OnGlassesOff += FadeIn;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOff -= FadeIn;
    }

    public void Disable()
    {
        gameObject.SetActive(keepActive);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        StartLerp(faded, solid, true);
    }

    public void FadeOut()
    {
        gameObject.SetActive(true);
        StartLerp(solid, faded, false);
    }


    private void StartLerp(Color a, Color b, bool isColliderActive)
    {
        enabled = true;
        elapsed = 0.0f;
        duration = CurveManager.AnimationDuration;

        this.a = a;
        this.b = b;
        this.isColliderActive = isColliderActive;
    }
    private void EndLerp()
    {
        enabled = false;
        ren.material.color = b;
        if (col != null)
            col.enabled = isColliderActive;
        gameObject.SetActive(keepActive);
    }
    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            t = CurveManager.FadeCurve.Evaluate(t);

            ren.material.color = Color.LerpUnclamped(a, b, t);
        }
        else
        {
            EndLerp();
        }
    }
}
