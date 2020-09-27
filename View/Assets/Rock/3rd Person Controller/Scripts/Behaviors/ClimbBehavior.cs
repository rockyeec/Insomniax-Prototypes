using UnityEngine;

public class ClimbBehavior : Behavior
{
    private readonly Vector3 rayOri = new Vector3(0.0f, 2.0f, 0.69f);
    private readonly float rayLength = 0.69f;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private bool isFirstFrame = true;
    private bool isClimbing = false;

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

        if (isClimbing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 6.9f * delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 20.0f * delta);

            bool isFinishedClimbing = transform.position == targetPos;
            if (isFinishedClimbing || !controller.Hold)
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
        if (Physics.Raycast(verticalRay, out RaycastHit hit0, rayLength, ~(1 << 20 | 1 << 9)))
        {
            Vector3 horizontalRayOri = transform.position;
            horizontalRayOri.y = hit0.point.y - 0.01f;

            //if (Vector3.Dot(hit0.normal, Vector3.up) != 1.0f)
            //    return;


            //Debug.DrawRay(horizontalRayOri,transform.forward);
            //Debug.LogError("hit");
            if (Physics.Raycast(horizontalRayOri, transform.forward, out RaycastHit hit1, 1.0f, ~(1 << 20 | 1 << 9)))
            {
                if (Mathf.Abs(Vector3.Dot(hit1.normal, Vector3.up)) >= 0.01f)
                    return;

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
