﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundBehavior : Behavior
{
    public override void Execute(CharacterController controller, in float delta)
    {
        Transform transform = controller.CharTransform;
        CharacterController.CustomOutputs outputs = controller.outputs;

        Ray ray = new Ray(transform.position + 1.0f * Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.05f))
        {
            transform.position = hit.point;
            outputs.onGround = true;
        }
        else
        {
            outputs.onGround = false;
        }
    }
}
