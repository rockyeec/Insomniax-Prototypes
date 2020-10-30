using System.Collections;
using UnityEngine;

public class WallLerper : MonoBehaviour
{
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
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;

        col = GetComponentInChildren<Collider>();
        ren = GetComponentInChildren<MeshRenderer>();
        if (ren != null)
        {
            ren.materials = new Material[0];
            ren.material = SolidMaterial;
        }

        solid = SolidMaterial.color;
        faded = SolidMaterial.color.WithAlpha(0.0f);

        duration = CurveManager.AnimationDuration;
        enabled = false;
    }

    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }


    public void GameScript_OnGlassesOff()
    {
        gameObject.SetActive(true);
        StartLerp(faded, solid, true);
    }

    public void GameScript_OnGlassesOn()
    {
        gameObject.SetActive(true);
        StartLerp(solid, faded, false);
    }


    private void StartLerp(Color a, Color b, bool isColliderActive)
    {
        enabled = true;
        elapsed = 0.0f;

        this.a = a;
        this.b = b;
        this.isColliderActive = isColliderActive;
    }
    private void EndLerp()
    {
        enabled = false;
        if (ren != null)
            ren.material.color = b;
        if (col != null)
            col.enabled = isColliderActive;
    }
    private void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (ren != null)
            {
                ren.material.color = Color.LerpUnclamped(a, b, CurveManager.GlassesAnimT);
            }
        }
        else
        {
            EndLerp();
        }
    }
}
