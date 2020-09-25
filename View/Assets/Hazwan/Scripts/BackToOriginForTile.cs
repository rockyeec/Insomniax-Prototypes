using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOriginForTile : BackToOriginal
{
    FallingTile tile;
    Rigidbody rb;
    bool wasFake = false;
    override protected void Start()
    {
        base.Start();
        tile = GetComponent<FallingTile>();
        rb = GetComponent<Rigidbody>();
    }
    protected override void OnStartLerp()
    {
        base.OnStartLerp();
        rb.isKinematic = true;

        wasFake = tile.isFake;
        tile.isFake = false;
    }
    protected override void OnEndLerp()
    {
        base.OnEndLerp();

        tile.isFake = wasFake;
    }
}
