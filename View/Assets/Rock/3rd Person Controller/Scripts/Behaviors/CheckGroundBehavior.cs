using UnityEngine;

public class CheckGroundBehavior : Behavior
{
    private readonly float durationCanJump = 0.3f;
    private float timeOffGround = 0.0f;

    public override void Execute(CharacterController controller, in float delta)
    {
        Transform transform = controller.CharTransform;
        CharacterController.CustomOutputs outputs = controller.outputs;
        CharacterController.CustomInputs inputs = controller.inputs;

        bool prevOnGround = outputs.onGround;

        Ray ray = new Ray(transform.position + 1.0f * Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.01f))
        {
            transform.position = hit.point;
            outputs.onGround = true;
            inputs.canJump = true;
        }
        else if (prevOnGround)
        {
            outputs.onGround = false;
            timeOffGround = Time.time + durationCanJump;
            inputs.canJump = true;
        }
        else
        {
            if (Time.time >= timeOffGround)
            {
                inputs.canJump = false;
            }
        }
    }
}
