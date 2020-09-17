using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    public void Init(Animator a, CharacterController c)
    {
        animator = a;
        controller = c;
    }

    public void Tick(/*in float delta*/)
    {
        if (controller.outputs.animateJump)
        {
            controller.outputs.animateJump = false;
            animator.CrossFade("Jump", 0.15f);
        }

        if (controller.outputs.animateClimb)
        {
            controller.outputs.animateClimb = false;
            animator.CrossFade("Ledge Up", 0.15f);
        }
    }

    public void FixedTick(in float delta)
    {
        animator.SetFloat("vertical", controller.outputs.vertical, 0.08f, delta);

        animator.SetFloat("horizontal", controller.outputs.horizontal, 0.08f, delta);

        animator.SetFloat("rotationDelta", controller.outputs.deltaRot);

        animator.SetBool("grounded", controller.outputs.onGround);

        animator.SetBool("falling", controller.Rb.velocity.y < -0.1f);
    }

    public void SetHold(in bool b)
    {
        controller.Hold = b;
    }
}
