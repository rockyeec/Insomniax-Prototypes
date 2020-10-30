using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLerper : DelayedWallLerper
{
    protected override Material SolidMaterial { get { return MaterialManager.FloorMaterial; } }
}
