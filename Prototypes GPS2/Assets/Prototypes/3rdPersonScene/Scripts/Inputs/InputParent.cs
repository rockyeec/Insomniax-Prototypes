using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParent : MonoBehaviour
{
    protected CharacterController Controller { get; private set; }
    private AnimatorHook animatorHook = null;

    private void Awake()
    {
        Controller = new CharacterController(gameObject.AddComponent<Rigidbody>());
        Animator anim = transform.GetComponentInChildren<Animator>();
        animatorHook = anim.gameObject.AddComponent<AnimatorHook>();
        animatorHook.Init(anim, Controller);
        Init();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        Tick(delta);
        Controller.Tick(in delta);
        animatorHook.Tick(/*in delta*/);
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        FixedTick(in delta);
        Controller.FixedTick(in delta);
        animatorHook.FixedTick(in delta);
    }
    private void LateUpdate()
    {
        float delta = Time.deltaTime;
        LateTick(in delta);
        //controller.LateTick(delta);
    }

    protected virtual void Init() { }
    protected virtual void Tick(in float delta) { }
    protected virtual void FixedTick(in float delta) { }
    protected virtual void LateTick(in float delta) { }
}
