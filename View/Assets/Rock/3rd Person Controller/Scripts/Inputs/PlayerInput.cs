using UnityEngine;

public class PlayerInput : InputParent
{
    [SerializeField] private ButtonScript jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;

    [SerializeField] private bool isCanClimb = true;
    [SerializeField] private bool isLevelHasMovingPlatforms = false;

    private Transform hips;

    protected override void Init()
    {
        base.Init();

        if (isCanClimb)
            Controller.AddFixedTickBehavior(new ClimbBehavior());

        if (isLevelHasMovingPlatforms)
            Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        else
            Controller.AddFixedTickBehavior(new CheckGroundWithNoMovingPlatformBehavior());

        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());

        Animator anim = GetComponentInChildren<Animator>();
        anim.gameObject.AddComponent<IKHandler>();
        hips = anim.GetBoneTransform(HumanBodyBones.Hips);
    }

    protected override void Tick(in float delta)
    {
        base.Tick(delta);        

        if (IsDisabled)
            return;

        // locomotion
        Controller.inputs.SmoothMoveInput(
            cam.transform.rotation 
            * new Vector3(
                leftJoy.GetHorizontal(),
                0.0f,
                leftJoy.GetVertical()
                ),
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
