using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbBehavior : RegularTickBehavior
{
    private readonly Vector3 rayOri = new Vector3(0.0f, 2.0f, 0.69f);
    private readonly float rayLength = 1.337f;
    private Vector3 targetPos;
    private bool isFirstFrame = true;

    private void Init(CharacterController controller)
    {
        isFirstFrame = false;
        targetPos = controller.CharTransform.position;
    }

    public override void Execute(CharacterController controller, in float delta)
    {
        if (isFirstFrame)
            Init(controller);

        Transform transform = controller.CharTransform;
        if (controller.Hold)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 6.9f * delta);

            return;
        }

        
        Debug.DrawRay(transform.rotation * rayOri + transform.position, Vector3.down * rayLength);
        Ray ray = new Ray(transform.rotation * rayOri + transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, 1 << 0))
        {
            controller.outputs.animateClimb = true;
            targetPos = hit.point;
        }
        else
        {
            targetPos = transform.position;
        }
    }
}
