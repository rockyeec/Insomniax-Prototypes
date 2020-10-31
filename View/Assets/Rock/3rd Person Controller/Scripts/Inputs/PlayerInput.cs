using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : InputParent
{
    [SerializeField] private Button glassesButton = null;
    [SerializeField] private ButtonScript jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;
    [SerializeField] private Slider camSensitivitySlider = null;

    [SerializeField] private bool isCanClimb = true;
    [SerializeField] private bool isLevelHasMovingPlatforms = false;
    [SerializeField] private bool isLevelHasSlopes = false;
    [SerializeField] private bool isLevelHasInteractables = false;

    public static float MoveSpeed { get; set; }
    public static bool IsEnableCamera { get; set; }
    public static bool IsCanMove { get; set; }

    private Transform hips;

    public event System.Action OnGlassesButtonPress = delegate { };

    protected override void Init()
    {
        base.Init();

        AssignBehaviors();
        AssignAnimatorComponents();
        AssignProperties();

        glassesButton.onClick.AddListener(PressGlassesButton);

        if (!PlayerPrefs.HasKey("camSensitivity"))
        {
            PlayerPrefs.SetFloat("camSensitivity", 1.0f);
        }
        camSensitivitySlider.value = PlayerPrefs.GetFloat("camSensitivity");
        camSensitivitySlider.onValueChanged.AddListener(SetCameraSensitivity);
    }

    public void PressGlassesButton()
    {
        OnGlassesButtonPress();
    }

    protected override void Tick(in float delta)
    {
        if (!IsCanMove)
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
        {
            cam.transform.position = hips.position;
            return;
        }

        base.FixedTick(delta);

        // camera
        cam.Tick(
            rightJoy.GetVerticalDelta() * PlayerPrefs.GetFloat("camSensitivity"),
            rightJoy.GetHorizontalDelta() * PlayerPrefs.GetFloat("camSensitivity"),
            hips.position,
            delta);
    }

    protected override void OnGamePause()
    {
        base.OnGamePause();
        leftJoy.Release();
        rightJoy.Release();
        jumpButton.Release();
    }


    private void AssignBehaviors()
    {
        if (isCanClimb)
            Controller.AddFixedTickBehavior(new ClimbBehavior());

        if (isLevelHasMovingPlatforms)
            Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        else
            Controller.AddFixedTickBehavior(new CheckGroundWithNoMovingPlatformBehavior());

        if (isLevelHasInteractables)
            Controller.AddFixedTickBehavior(new InteractBehavior());

        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());
    }

    private void AssignAnimatorComponents()
    {
        Animator anim = GetComponentInChildren<Animator>();
        hips = anim.GetBoneTransform(HumanBodyBones.Hips);

        if (isLevelHasSlopes)
            anim.gameObject.AddComponent<IKHandler>();
    }

    private void AssignProperties()
    {
        MoveSpeed = 1.0f;
        IsEnableCamera = true;
        IsCanMove = true;
    }

    private void SetCameraSensitivity(float value)
    {
        PlayerPrefs.SetFloat("camSensitivity", value);
    }
}
