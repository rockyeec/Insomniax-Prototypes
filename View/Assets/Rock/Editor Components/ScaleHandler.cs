using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScaleHandler : MonoBehaviour
{
    [SerializeField] Vector3 scale = Vector3.one;
    [SerializeField] float colliderVsRendererScaleFactor = 1.0f;
    [SerializeField] bool applyChanges = false;
    // Update is called once per frame
    void Update()
    {
        if (!applyChanges)
            return;

        applyChanges = false;

        BoxCollider collider = GetComponentInChildren<BoxCollider>();
        if (collider != null)
        {
            collider.size = scale;
        }

        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        if (rend != null)
        {
            rend.transform.localScale = scale * colliderVsRendererScaleFactor;
        }
    }
}
