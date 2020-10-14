using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : InputParent
{
    [SerializeField] private Button glassesButton = null;
    [SerializeField] private ButtonScript jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;

    [SerializeField] private bool isCanClimb = true;
    [SerializeField] private bool isLevelHasMovingPlatforms = false;
    [SerializeField] private bool isLevelHasSlopes = false;

    public static float MoveSpeed { get; set; }

    public static bool IsEnableCamera { get; set; }

    private Transform hips;

    protected override void Init()
    {
        base.Init();
        MoveSpeed = 1.0f;
        IsEnableCamera = true;

        glassesButton.onClick.AddListener(GameScript.PutOnGlasses);

        if (isCanClimb)
            Controller.AddFixedTickBehavior(new ClimbBehavior());

        if (isLevelHasMovingPlatforms)
            Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        else
            Controller.AddFixedTickBehavior(new CheckGroundWithNoMovingPlatformBehavior());

        //Controller.AddFixedTickBehavior(new InteractBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());

        Animator anim = GetComponentInChildren<Animator>();
        hips = anim.GetBoneTransform(HumanBodyBones.Hips);

        if (isLevelHasSlopes)
            anim.gameObject.AddComponent<IKHandler>();
    }

    protected override void Tick(in float delta)
    {
        if (!IsEnableCamera)
        {
            Controller.inputs.SmoothMoveInput(Vector3.zero, delta);
            return;
        }

        if (IsDisabled)
            return;

        base.Tick(delta);    
        
        // locomotion
        Controller.inputs.SmoothMoveInput(
            cam.transform.rotation 
            * new Vector3(
                leftJoy.GetHorizontal(),
                0.0f,
                leftJoy.GetVertical()
                )
            * MoveSpeed,
            delta);
        
        // jump
        if (jumpButton.IsDown)
        {
            Controller.inputs.jump = true;
        }

        if (jumpButton.IsUp)
        {
            Controller.inputs.jumpRelease = true;
        }
    }

    protected override void FixedTick(in float delta)
    {
        if (!IsEnableCamera)
            return;

        base.FixedTick(delta);

        // camera
        cam.Tick(
            rightJoy.GetVerticalDelta(),
            rightJoy.GetHorizontalDelta(),
            hips.position,
            //transform.position,
            delta);
    }

    protected override void OnGamePause()
    {
        base.OnGamePause();
        leftJoy.Release();
        rightJoy.Release();
        jumpButton.Release();
    }

}
