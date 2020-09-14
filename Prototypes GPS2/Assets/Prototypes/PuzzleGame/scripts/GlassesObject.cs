using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GlassesObject : MonoBehaviour
{
    public abstract void Activate(in float lerpRate);
    public abstract void Deactivate(in float lerpRate);


    protected float lerpRate = 6.9f;
}
