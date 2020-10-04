using UnityEngine;

public class CheckGroundBehavior : Behavior
{
    private readonly float durationCanJump = 0.35f;
    private float timeOffGround = 0.0f;

    public override void Execute(CharacterController controller, in float delta)
    {
        Transform transform = controller.CharTransform;

        bool prevOnGround = controller.outputs.onGround;

        Ray ray = new Ray(
            transform.position + 1.0f * Vector3.up,
            Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 1.69f, ~(1 << 30)))
        {
            controller.outputs.inclination = Vector3.Dot(transform.forward, hit.normal);

            bool isFeetOnGround = hit.distance <= 1.0f;
            if (isFeetOnGround)
            {
                HandleOnGround(ref controller, in hit);
                return;
            }

            bool isDecline = controller.outputs.inclination > 0.0f;
            bool isMoving = controller.inputs.MoveDir != Vector3.zero;
            bool isJumping = controller.Rb.velocity.y >= 0.0f;

            if (isDecline && isMoving && !isJumping && prevOnGround)
            {
                HandleOnGround(ref controller, in hit);
                return;
            }
        }
        HandleNotOnGround(ref controller, in prevOnGround);        
    }

    private void HandleOnGround(ref CharacterController controller, in RaycastHit hit)
    {
        Transform transform = controller.CharTransform;

        transform.position = hit.point;
        controller.outputs.onGround = true;
        controller.inputs.canJump = true;

        transform.SetParent(hit.collider.transform);
        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
    }

    private void HandleNotOnGround(ref CharacterController controller, in bool prevOnGround)
    {       
        if (prevOnGround) 
        {
            timeOffGround = Time.time + durationCanJump;
            controller.outputs.onGround = false;
            controller.inputs.canJump = true;

            controller.CharTransform.SetParent(null);
        }
        else if (Time.time >= timeOffGround)
        {
            controller.inputs.canJump = false;
        }
    }
}
