using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierObject : GlassesObject
{
    [SerializeField] Material[] materials = new Material[2];

    private MeshRenderer meshRenderer = null;
    private MeshCollider meshCollider = null;
    private int targetMaterial = 1;

    public override void Activate(in float lerpRate)
    {
        targetMaterial = 0;
        meshCollider.enabled = false;
    }

    public override void Deactivate(in float lerpRate)
    {
        targetMaterial = 1;
        meshCollider.enabled = true;
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer.material = materials[0];
    }

    private void Update()
    {
        meshRenderer.material.Lerp(materials[1 - targetMaterial], materials[targetMaterial], lerpRate * Time.deltaTime);
    }
}
