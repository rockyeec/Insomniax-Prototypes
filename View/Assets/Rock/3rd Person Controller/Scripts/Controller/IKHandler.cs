using UnityEngine;

public class IKHandler : MonoBehaviour
{
    private Animator animator;
    private float curWeight = 0.0f;

    public class IKBodyPart
    {
        public Transform IkTransform { get; private set; }
        public AvatarIKGoal Goal { get; private set; }
        public float Height { get; private set; }
        public float Pitch { get; private set; }
        public IKBodyPart(in Transform transform, in AvatarIKGoal goal, in float distanceFromGround, in float pitch)
        {
            IkTransform = transform;
            Goal = goal;
            Height = distanceFromGround;
            Pitch = pitch * 0.1f; // arbitrary value, since cant seem to figure out actual value yet
        }
    }
    private IKBodyPart[] feet = new IKBodyPart[2];



    private void Start()
    {
        animator = GetComponent<Animator>();

        Transform leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Transform rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

        feet[0] = new IKBodyPart(
            leftFoot, 
            AvatarIKGoal.LeftFoot,
            transform.InverseTransformPoint(leftFoot.position).y,
            transform.InverseTransformDirection(leftFoot.up).y);
        feet[1] = new IKBodyPart(
            rightFoot,
            AvatarIKGoal.RightFoot,
            transform.InverseTransformPoint(rightFoot.position).y,
            transform.InverseTransformDirection(rightFoot.up).y);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float targetWeight = CanPerformIK() ? 1.0f : 0.0f;
        curWeight = Mathf.MoveTowards(curWeight, targetWeight, Time.deltaTime * 6.9f);

        foreach (IKBodyPart foot in feet)
        {
            animator.SetIKPositionWeight(foot.Goal, curWeight);
            animator.SetIKRotationWeight(foot.Goal, curWeight);

            if (curWeight == 0.0f)
                continue;

            Debug.DrawRay(foot.IkTransform.position + Vector3.up * 0.3f, Vector3.down * 0.6f);
            if (Physics.Raycast(
                foot.IkTransform.position + Vector3.up * 0.3f,
                Vector3.down, 
                out RaycastHit hit,
                0.6f))
            {
                animator.SetIKPosition(foot.Goal, hit.point + Vector3.up * foot.Height);

                Vector3 dir = foot.IkTransform.up;
                dir.y = foot.Pitch;
                animator.SetIKRotation(foot.Goal, Quaternion.LookRotation(dir, hit.normal));
            }
        }
    }

    private bool CanPerformIK()
    {
        return Mathf.Abs( animator.GetFloat("vertical")) < 0.03f;
    }
}
