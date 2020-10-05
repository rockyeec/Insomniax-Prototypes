using System.Collections.Generic;
using UnityEngine;

public class InteractBehavior : Behavior
{
    readonly List<InteractableObject> oldInteractables = new List<InteractableObject>();
    public override void Execute(CharacterController controller, in float delta)
    {
        Transform transform = controller.Transform;

        Collider[] colliders = Physics.OverlapSphere(
                transform.position + Vector3.up + transform.forward * 0.26f, 1.0f,
                1 << 10);
        if (colliders.Length > 0)
        {
            foreach (var item in colliders)
            {
                InteractableObject interactable = item.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    if (!oldInteractables.Contains(interactable))
                    {
                        oldInteractables.Add(interactable);
                        interactable.MakeOutline();
                    }
                }
            }
        }
        else
        {
            ClearOldInteractables();
        }
    }

    void ClearOldInteractables()
    {
        foreach (var item in oldInteractables)
        {
            item.MakeStandard();
        }
        oldInteractables.Clear();
    }
}
