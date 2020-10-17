using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLerper : WallLerper
{
    protected override Material SolidMaterial { get { return MaterialManager.CeilingMaterial; } }
}
