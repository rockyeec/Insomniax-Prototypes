using UnityEngine;

public class InputParent : ViewEntity
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

    protected override void UpdateEntity()
    {
        float delta = Time.deltaTime;
        Tick(delta);
        Controller.Tick(in delta);
        animatorHook.Tick(/*in delta*/);
    }
    protected override void FixedUpdateEntity()
    {
        float delta = Time.fixedDeltaTime;
        FixedTick(in delta);
        Controller.FixedTick(in delta);
        animatorHook.FixedTick(in delta);
    }
    protected override void LateUpdateEntity()
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
