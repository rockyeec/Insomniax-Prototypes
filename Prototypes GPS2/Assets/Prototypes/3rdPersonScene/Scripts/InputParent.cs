using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParent : MonoBehaviour
{
    protected CharacterController controller { get; private set; }
    private AnimatorHook animatorHook = null;

    private void Awake()
    {
        controller = new CharacterController(gameObject.AddComponent<Rigidbody>());
        Animator anim = transform.GetComponentInChildren<Animator>();
        animatorHook = anim.gameObject.AddComponent<AnimatorHook>();
        animatorHook.Init(anim, controller);
        Init();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        Tick(delta);
        controller.Tick(delta);
        animatorHook.Tick(delta);
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        FixedTick(delta);
        controller.FixedTick(delta);
        animatorHook.FixedTick(delta);
    }
    private void LateUpdate()
    {
        float delta = Time.deltaTime;
        LateTick(delta);
        controller.LateTick(delta);
    }

    protected virtual void Init() { }
    protected virtual void Tick(float delta) { }
    protected virtual void FixedTick(float delta) { }
    protected virtual void LateTick(float delta) { }
}
