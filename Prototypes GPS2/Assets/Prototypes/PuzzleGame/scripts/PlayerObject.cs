using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : GlassesObject
{
    public override void Activate(in float lerpRate)
    {
        isMovingToOri = false;
    }

    public override void Deactivate(in float lerpRate)
    {
        isMovingToOri = true;
        this.lerpRate = lerpRate;
    }

    private Vector3 startPos;
    private Quaternion startRot;
    private bool isMovingToOri = false;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingToOri)
        {
            if (Vector3.Distance(startPos, transform.position) < 0.3f
                && Vector3.Distance(startRot.eulerAngles, transform.eulerAngles) < 0.3f)
            {
                isMovingToOri = false;
            }

            transform.position = Vector3.Lerp(transform.position, startPos, lerpRate * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRot, lerpRate * Time.deltaTime);
        }
    }
}
