using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLerper : DelayedWallLerper
{
    protected override Material SolidMaterial { get { return MaterialManager.CeilingMaterial; } }
}
