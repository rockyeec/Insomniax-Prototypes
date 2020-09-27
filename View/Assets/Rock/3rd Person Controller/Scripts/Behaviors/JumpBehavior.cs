using UnityEngine;

public class JumpBehavior : Behavior
{
    private float hasJumpedTime = 0.0f;
    private readonly float canJumpAgainDuration = 0.4f;

    public override void Execute(CharacterController controller, in float delta)
    {
        if (controller.inputs.jumpRelease)
        {
            controller.inputs.jumpRelease = false;

            if (controller.Hold)
                return;

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

        if (controller.Hold)
            return;

        if (!controller.inputs.canJump)
            return;

        if (Time.time < hasJumpedTime)
            return;
        hasJumpedTime = Time.time + canJumpAgainDuration;

        Rigidbody rb = controller.Rb;
        Vector3 zeroGravityVel = rb.velocity;
        zeroGravityVel.y = 0.0f;
        rb.velocity = zeroGravityVel;
        rb.AddForce(Vector3.up * 6.9f, ForceMode.Impulse);
        
        controller.outputs.animateJump = true;
        AudioManager.instance.Play("Shoot", "SFX");
    }
}
