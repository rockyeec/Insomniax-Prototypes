using UnityEngine;

public class PlayerInput : InputParent
{
    [SerializeField] private ButtonScript jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;


    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());
        Controller.AddRegularTickBehavior(new ClimbBehavior());
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

    protected override void LateTick(in float delta)
    {
        base.LateTick(delta);
        // camera
        cam.Tick(
            rightJoy.GetVerticalDelta(),
            rightJoy.GetHorizontalDelta(),
            transform.position,
            delta);
    }
}
