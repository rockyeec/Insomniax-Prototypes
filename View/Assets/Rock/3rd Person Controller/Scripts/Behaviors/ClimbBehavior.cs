using UnityEngine;

public class ClimbBehavior : Behavior
{
    private readonly Vector3 rayOri = new Vector3(0.0f, 2.0f, 0.69f);
    private readonly float rayLength = 0.69f;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private bool isFirstFrame = true;
    private bool isClimbing = false;
    private Transform transform;

    private void Init(CharacterController controller)
    {
        isFirstFrame = false;
        targetPos = controller.CharTransform.position;
        transform = controller.CharTransform;
    }

    private bool IsFinishedClimbing()
    {
        return transform.position == targetPos;
    }

    public override void Execute(CharacterController controller, in float delta)
    {
        if (isFirstFrame)
            Init(controller);

        if (isClimbing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 6.9f * delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 20.0f * delta);
                        
            if (IsFinishedClimbing())
            {
                controller.Rb.isKinematic = false;
                isClimbing = false;
            }

            return;
        }


        if (controller.outputs.onGround)
        {
            return;
        }
        
        //Debug.DrawRay(transform.rotation * rayOri + transform.position, Vector3.down * rayLength);
        Ray verticalRay = new Ray(transform.rotation * rayOri + transform.position, Vector3.down);
        if (Physics.Raycast(verticalRay, out RaycastHit hit0, rayLength, ~(1 << 20)))
        {
            Vector3 horizontalRayOri = transform.position;
            horizontalRayOri.y = hit0.point.y - 0.01f;

            //Debug.DrawRay(horizontalRayOri,transform.forward);

            if (Physics.Raycast(horizontalRayOri, transform.forward, out RaycastHit hit1, 1.0f, ~(1 << 20)))
            {
                isClimbing = true;
                controller.outputs.animateClimb = true;
                controller.Rb.isKinematic = true;
                
                targetRot = Quaternion.LookRotation(-hit1.normal);
                
                targetPos = hit1.point - hit1.normal * 0.01f;
                targetPos.y = hit0.point.y;
            }
        }
    }
}
