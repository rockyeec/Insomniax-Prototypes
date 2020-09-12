using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : FixedTickBehavior
{
    public override void Execute(CharacterController controller, float delta)
    {
        if (!controller.inputs.jump)
            return;

        controller.inputs.jump = false;

        if (controller.outputs.onGround 
            && !controller.outputs.jump)
        {
            controller.inputs.jump = false;
            controller.outputs.jump = true;
        }
    }
}
