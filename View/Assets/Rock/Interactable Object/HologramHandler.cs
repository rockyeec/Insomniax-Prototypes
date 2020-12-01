using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HologramHandler : MonoBehaviour
{
    Renderer rend;
    Material[] originalMaterials;
    Material[] hologramMaterials;
    YieldInstruction waitForEndOfFrame = new WaitForEndOfFrame();

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalMaterials = rend.materials;
    }

    IEnumerator TryMakeHolo()
    {        
        if (hologramMaterials == null)
        {
            while (MaterialManager.OutLineMaterial == null)
            {
                print("fuck");
                yield return waitForEndOfFrame;
            }
            hologramMaterials = new Material[1] { MaterialManager.OutLineMaterial };
        }

        rend.materials = hologramMaterials;
    }

    public void MakeHolo()
    {
        StartCoroutine(TryMakeHolo());
    }
    public void MakeSolid()
    {
        rend.materials = originalMaterials;
    }
}
