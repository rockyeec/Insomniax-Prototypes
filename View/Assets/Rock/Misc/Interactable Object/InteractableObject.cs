using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractableObject : MonoBehaviour
{
    private Renderer[] rens = null;
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
        rens = GetComponentsInChildren<Renderer>();
        List<Material> materials = new List<Material>();
        foreach (var ren in rens)
        {
            foreach (var item in ren.materials)
            {
                materials.Add(item);
            }
        }
        materials.Add(MaterialManager.OutLineMaterial);

        outlineMaterials = materials.ToArray();
        standardMaterials = rens.FirstOrDefault().materials;
    }

    public void MakeOutline()
    {
        foreach (var item in rens)
        {
            item.materials = outlineMaterials;
        }
    }
    public void MakeStandard()
    {
        foreach (var item in rens)
        {
            item.materials = standardMaterials;
        }
    }
}
