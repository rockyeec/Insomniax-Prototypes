using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Renderer ren = null;
    private Material[] outlineMaterials = null;
    private Material[] standardMaterials = null;
    private void Start()
    {
        StartCoroutine(WaitStart());
        gameObject.layer = 10;
    }
    private IEnumerator WaitStart()
    {
        yield return null;
        ren = GetComponentInChildren<Renderer>();
        List<Material> materials = new List<Material>();
        foreach (var item in ren.materials)
        {
            materials.Add(item);
        }
        materials.Add(MaterialManager.OutLineMaterial);

        outlineMaterials = materials.ToArray();
        standardMaterials = ren.materials;
    }

    public void MakeOutline()
    {
        ren.materials = outlineMaterials;
    }
    public void MakeStandard()
    {
        ren.materials = standardMaterials;
    }
}
