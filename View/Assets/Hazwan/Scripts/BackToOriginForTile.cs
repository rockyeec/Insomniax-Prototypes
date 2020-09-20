using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOriginForTile : BackToOriginal
{
    Rigidbody rb;
    override protected void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }
    protected override void OnStartLerp()
    {
        base.OnStartLerp();
        rb.isKinematic = true;
    }
    protected override void OnEndLerp()
    {
        base.OnEndLerp();
    }
}
