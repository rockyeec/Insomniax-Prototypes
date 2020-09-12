using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deployable : MonoBehaviour
{
    [Header("Deploy Attributes")]
    [SerializeField] private Material[] materials = null;

    protected bool IsDeployed { get; private set; }
    private Camera cam;

    private MeshRenderer[] renderers;
    private MeshCollider[] colliders;

    public enum RendererIndex
    {
        normal = 0,
        transparent_green = 1,
        transparent_red = 2
    }

    void ChangeMaterial(RendererIndex index)
    {
        foreach (var item in renderers)
        {
            item.material = materials[(int)index];
        }
    }
    void SetColliders(bool b)
    {
        foreach (var item in colliders)
        {
            item.enabled = b;
        }
    }

    private void Start()
    {
        cam = Camera.main;
        renderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<MeshCollider>();
        IsDeployed = false;
        SetColliders(false);
    }

    protected virtual void Update()
    {
        if (IsDeployed)
            return;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            bool isDeployable = hit.collider.gameObject.layer == 31;
            
            transform.position = hit.point;            

            if (Input.GetMouseButtonUp(0))
            {
                if (isDeployable)
                {
                    Deploy();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                ChangeMaterial(
                isDeployable
                ? RendererIndex.transparent_green
                : RendererIndex.transparent_red);
            }
        }
    }

    void Deploy()
    {
        ChangeMaterial(RendererIndex.normal);
        IsDeployed = true;
        SetColliders(true);
    }
}
