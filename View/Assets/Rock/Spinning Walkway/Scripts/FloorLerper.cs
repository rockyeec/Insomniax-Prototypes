using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLerper : WallLerper
{
    protected override Material SolidMaterial { get { return MaterialManager.FloorMaterial; } }
}
