using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    private bool hasJumped = false;

    public void Init(Animator a, CharacterController c)
    {
        animator = a;
        controller = c;
    }

    public void Tick(float delta)
    {
        if (controller.outputs.jump && !hasJumped)
        {
            hasJumped = true;

            animator.CrossFade("Jump", 0.1f);
        }
    }

    public void FixedTick(float delta)
    {
        animator.SetFloat("vertical", controller.outputs.vertical, 0.08f, delta);

        animator.SetFloat("horizontal", controller.outputs.horizontal, 0.08f, delta);

        animator.SetFloat("rotationDelta", controller.outputs.deltaRot);

        animator.SetBool("grounded", controller.outputs.onGround);

        animator.SetBool("falling", controller.rb.velocity.y < -0.1f);
    }


    public void Jump()
    {
        StartCoroutine(JumpAtFixedFrame());
    }
    IEnumerator JumpAtFixedFrame()
    {
        yield return new WaitForFixedUpdate();

        controller.rb.AddForce(Vector3.up * 8.8f, ForceMode.Impulse);
        hasJumped = false;
        controller.outputs.jump = false;
    }
    public void SetHold(bool b)
    {
        controller.hold = b;
    }
}
