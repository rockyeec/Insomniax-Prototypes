using UnityEngine;

public class PlayerInput : InputParent
{
    [SerializeField] private ButtonScript jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;

    private bool isLerpingToOrigin = false;
    private Quaternion originalRot;
    private Vector3 originalPos;


    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());
        Controller.AddRegularTickBehavior(new ClimbBehavior());

        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    protected override void Tick(in float delta)
    {
        base.Tick(delta);        

        // camera
        cam.LookAround(
            rightJoy.GetVerticalDelta(),
            rightJoy.GetHorizontalDelta(),
            transform.position,
            delta);

        // glasses reset effect
        HandleLerping(in delta);

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

    protected override void OnGlassesOff()
    {
        DisableController();
        isLerpingToOrigin = true;
    }

    private void DisableController()
    {
        Controller.Hold = true;
        Controller.outputs.vertical = 0.0f;
        Controller.outputs.horizontal = 0.0f;
        Controller.Rb.useGravity = false;
    }
    private void EnableController()
    {
        Controller.Hold = false;
        Controller.Rb.useGravity = true;
    }
    private void HandleLerping(in float delta)
    {
        if (!isLerpingToOrigin)
            return;

        transform.position = Vector3.Lerp(transform.position, originalPos, delta * lerpRate);
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRot, delta * lerpRate);
        if (transform.position.IsCloseTo(originalPos, 2.5f))
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
            EnableController();
            isLerpingToOrigin = false;
        }
    }
}
