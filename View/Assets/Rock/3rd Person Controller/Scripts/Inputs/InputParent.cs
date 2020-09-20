using UnityEngine;

public abstract class InputParent : MonoBehaviour
{
    protected CharacterController Controller { get; private set; }
    private AnimatorHook animatorHook = null;
    private bool isPause;
    protected bool IsDisabled { get; private set; }

    private void Awake()
    {
        Controller = new CharacterController(gameObject.AddComponent<Rigidbody>());
        Animator anim = transform.GetComponentInChildren<Animator>();
        animatorHook = anim.gameObject.AddComponent<AnimatorHook>();
        animatorHook.Init(anim, Controller);
        Init();
    }
    private void Start()
    {
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;

        gameObject.AddComponent<BackToOriginal>();
    }
    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;
    }

    private void GameScript_OnUnpause()
    {
        isPause = false;
    }

    private void GameScript_OnPause()
    {
        isPause = true;
    }

    private void Update()
    {
        if (isPause)
            return;

        float delta = Time.deltaTime;
        Tick(delta);
        Controller.Tick(in delta);
        animatorHook.Tick(/*in delta*/);
    }
    private void FixedUpdate()
    {
        if (isPause)
            return;

        float delta = Time.fixedDeltaTime;
        FixedTick(in delta);
        Controller.FixedTick(in delta);
        animatorHook.FixedTick(in delta);
    }
    private void LateUpdate()
    {
        if (isPause)
            return;

        float delta = Time.deltaTime;
        LateTick(in delta);
        //controller.LateTick(delta);
    }

    protected virtual void Init() { }
    protected virtual void Tick(in float delta) { }
    protected virtual void FixedTick(in float delta) { }
    protected virtual void LateTick(in float delta) { }


    public void DisableController()
    {
        IsDisabled = true;
        Controller.Rb.isKinematic = true;
        Controller.outputs.vertical = 0.0f;
        Controller.outputs.horizontal = 0.0f;
    }
    public void EnableController()
    {
        IsDisabled = false;
        Controller.Rb.isKinematic = false;
    }
}
