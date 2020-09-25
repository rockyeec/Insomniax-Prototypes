using UnityEngine;

public class LocomotionBehavior : Behavior
{
    // temp
    readonly float speed = 369.0f;
    readonly float slerpRate = 6.9f;

    public override void Execute(CharacterController controller, in float delta)
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
        Vector3 moveDir = inputs.MoveDir;

        // for animator hook
        outputs.vertical = moveDir.sqrMagnitude;
        outputs.horizontal = Vector3.Dot(transform.right, moveDir);
        outputs.deltaRot = Vector3.Dot(transform.forward, moveDir);

        // rotation
        bool isMoving = outputs.vertical != 0.0f;
        if (isMoving)
        {
            Quaternion rot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, delta * slerpRate);
        }

        // wall
        if (isMoving
            && Physics.OverlapSphere(
                transform.position + Vector3.up + transform.forward * 0.26f, 0.24f,
                ~(1 << 31))
            .Length > 0)
        {
            moveDir = Vector3.zero;
            outputs.vertical = 0.0f;
        }
        
        // movement
        moveDir *= delta * speed * outputs.vertical;
        if (!outputs.onGround)
        {
            // move slower
            moveDir *= 0.69f;

            // fall
            moveDir.y = rb.velocity.y - 0.1337f;
        }
        rb.velocity = moveDir;
    }
}
