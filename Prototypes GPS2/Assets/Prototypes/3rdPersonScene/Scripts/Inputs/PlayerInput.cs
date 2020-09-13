using System.Collections;
using System.Collections.Generic;
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

    protected override void Tick(float delta)
    {
        base.Tick(delta);

        // camera
        cam.LookAround(
            rightJoy.GetVerticalDelta(), 
            rightJoy.GetHorizontalDelta(), 
            transform.position,
            delta);

        // locomotion
        float hor = leftJoy.GetHorizontal();
        float ver = leftJoy.GetVertical();
        Controller.inputs.SmoothMoveInput(cam.GetRotation() * new Vector3(hor, 0.0f, ver), delta);
        
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

    protected override void FixedTick(float delta)
    {
        base.FixedTick(delta);
    }

    protected override void LateTick(float delta)
    {
        base.LateTick(delta);
    }
}
