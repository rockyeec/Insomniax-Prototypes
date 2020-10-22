using UnityEngine;

public abstract class InputParent : MonoBehaviour
{
    [SerializeField] bool isBackToOriginal = true;
    protected CharacterController Controller { get; private set; }
    public Rigidbody Rb { get { return Controller.Rb; } }
    private AnimatorHook animatorHook = null;
    private bool isPause;
    protected bool IsDisabled { get; private set; }

    public event System.Action<float> OnTick = delegate { };
    public event System.Action<float> OnFixedTick = delegate { };

    private void Awake()
    {
        Controller = new CharacterController(gameObject.AddComponent<Rigidbody>());
        Animator anim = transform.GetComponentInChildren<Animator>();
        if (anim != null)
        {
            animatorHook = anim.gameObject.AddComponent<AnimatorHook>();
            animatorHook.Init(anim, Controller, this);
        }
        Init();
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnUnpause += GameScript_OnUnpause;

        if (isBackToOriginal)
            gameObject.AddComponent<BackToOriginalForCharacter>();
    }
    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnUnpause -= GameScript_OnUnpause;

        if (animatorHook != null)
            animatorHook.DeInit(this);
    }

    private void GameScript_OnUnpause()
    {
        isPause = false;
    }

    private void GameScript_OnPause()
    {
        isPause = true;
        OnGamePause();
    }

    protected virtual void OnGamePause() { }

    private void Update()
    {
        if (isPause)
            return;

        float delta = Time.deltaTime;
        Tick(delta);
        Controller.Tick(in delta);
        OnTick(delta);
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        FixedTick(in delta);
        Controller.FixedTick(in delta);
        OnFixedTick(delta);
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
