using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : FixedTickBehavior
{
    public override void Execute(CharacterController controller, float delta)
    {
        if (controller.inputs.jumpRelease)
        {
            controller.inputs.jumpRelease = false;
            if (controller.Rb.velocity.y >= 0.02f)
            {
                Vector3 rbVel = controller.Rb.velocity;
                rbVel.y /= 2.0f;
                controller.Rb.velocity = rbVel;
            }
        }

        if (!controller.inputs.jump)
            return;

        controller.inputs.jump = false;

        if (!controller.outputs.onGround)
            return;

        controller.outputs.animateJump = true;
        controller.Rb.AddForce(Vector3.up * 16.9f, ForceMode.Impulse);
    }
}
