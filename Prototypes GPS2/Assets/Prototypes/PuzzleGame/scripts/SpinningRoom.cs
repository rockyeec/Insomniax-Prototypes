using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningRoom : GlassesObject
{
    [SerializeField] private float spinRate = 69.0f;
    private bool isSpin = false;

    public override void Activate(in float lerpRate)
    {
        isSpin = true;
        this.lerpRate = lerpRate;
    }

    public override void Deactivate(in float lerpRate)
    {
        isSpin = false;
    }

    private void Update()
    {
        if (!isSpin)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * lerpRate);

            return;
        }

        transform.Rotate(transform.forward, Time.deltaTime * spinRate);
    }
}
