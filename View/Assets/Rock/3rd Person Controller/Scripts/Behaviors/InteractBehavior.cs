using System.Linq;
using UnityEngine;



public class InteractBehavior : Behavior
{
    InteractableObject oldInteractable = null;
    public override void Execute(CharacterController controller, in float delta)
    {
        Transform transform = controller.Transform;

        Collider nearest = Physics.OverlapSphere(
                transform.position + Vector3.up + transform.forward * 0.26f,
                1.0f,
                1 << 10)
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (nearest != null)
        {
            InteractableObject newInteractable = nearest.GetComponent<InteractableObject>();
            if (oldInteractable != newInteractable)
            {
                ClearOldInteractable();

                newInteractable.MakeOutline();
                oldInteractable = newInteractable;
            }
        }
        else
        {
            ClearOldInteractable();
        }

    }

    void ClearOldInteractable()
    {
        if (oldInteractable != null)
        {
            oldInteractable.MakeStandard();
            oldInteractable = null;
        }
    }
}
