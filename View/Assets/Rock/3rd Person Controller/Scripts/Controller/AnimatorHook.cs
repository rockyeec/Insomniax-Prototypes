﻿using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;


    public void Init(Animator a, CharacterController c, InputParent inputParent)
    {
        animator = a;
        controller = c;

        inputParent.OnFixedTick += InputParent_OnFixedTick; 
        inputParent.OnTick += InputParent_OnTick;
    }
    public void DeInit(InputParent inputParent)
    {
        inputParent.OnFixedTick -= InputParent_OnFixedTick;
        inputParent.OnTick -= InputParent_OnTick;
    }

    private void InputParent_OnTick(float delta)
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

    private void InputParent_OnFixedTick(float delta)
    {
        animator.SetFloat("vertical", controller.outputs.vertical, 0.08f, delta);

        animator.SetFloat("horizontal", controller.outputs.horizontal, 0.08f, delta);

        animator.SetFloat("incline", controller.outputs.inclination, 0.08f, delta);

        animator.SetFloat("rotationDelta", controller.outputs.deltaRot);

        animator.SetBool("grounded", controller.outputs.onGround);

        animator.SetBool("falling", controller.Rb.velocity.y < -0.1f);
    }


    public void SetHold(in bool b)
    {
        controller.Hold = b;
    }
}
