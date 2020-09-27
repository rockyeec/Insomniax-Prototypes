using UnityEngine;

public class ClimbBehavior : Behavior
{
    private readonly Vector3 rayOri = new Vector3(0.0f, 2.0f, 0.69f);
    private readonly float rayLength = 0.69f;
    private Transform charModelTrans = null;
    private Quaternion targetRot;
    private bool isFirstFrame = true;
    private bool isClimbing = false;

    private void Init(CharacterController controller)
    {
        isFirstFrame = false;
        charModelTrans = controller.CharTransform.GetComponentInChildren<Animator>().transform;
    }

    public override void Execute(CharacterController controller, in float delta)
    {
        if (isFirstFrame)
            Init(controller);

        Transform transform = controller.CharTransform;

        if (isClimbing)
        {
            charModelTrans.localPosition = Vector3.MoveTowards(charModelTrans.localPosition, Vector3.zero, 6.9f * delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 20.0f * delta);

            bool isFinishedClimbing =
                charModelTrans.localPosition == Vector3.zero
                 || !controller.Hold;

            if (isFinishedClimbing)
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
        if (Physics.Raycast(verticalRay, out RaycastHit hit0, rayLength, ~(1 << 20 | 1 << 9 | 1 << 30)))
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

                Vector3 prevPos = transform.position;
                Vector3 targetPos = hit1.point - hit1.normal * 0.01f;
                targetPos.y = hit0.point.y;
                transform.position = targetPos;
                charModelTrans.position = prevPos;
            }
        }
    }
}
