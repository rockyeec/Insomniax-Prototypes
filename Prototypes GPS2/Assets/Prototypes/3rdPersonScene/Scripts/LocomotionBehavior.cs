using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionBehavior : FixedTickBehavior
{
    // temp
    readonly float speed = 369.0f;
    readonly float slerpRate = 6.9f;

    public override void Execute(CharacterController controller, float delta)
    {
        if (controller.Hold)
        {
            controller.Rb.velocity = Vector3.zero;
            return;
        }

        CharacterController.CustomInputs inputs = controller.inputs;
        CharacterController.CustomOutputs outputs = controller.outputs;
        Transform transform = controller.CharTransform;
        Rigidbody rb = controller.Rb;

        Vector3 zeroPitchMoveDir = inputs.MoveDir;
        zeroPitchMoveDir.y = 0.0f;
        zeroPitchMoveDir.Normalize();

        // for animator hook
        outputs.vertical = inputs.MoveDir.sqrMagnitude;
        outputs.horizontal = Vector3.Dot(transform.right, zeroPitchMoveDir);
        outputs.deltaRot = Vector3.Dot(transform.forward, zeroPitchMoveDir);

        // rotation
        bool isMoving = outputs.vertical != 0.0f;
        if (isMoving)
        {
            Quaternion rot = Quaternion.LookRotation(zeroPitchMoveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, delta * slerpRate);
        }

        // movement
        zeroPitchMoveDir *= delta * speed * outputs.vertical;
        if (!outputs.onGround)
        {
            // fall
            zeroPitchMoveDir.y = rb.velocity.y - 0.420f;
        }
        rb.velocity = zeroPitchMoveDir;
    }
}
