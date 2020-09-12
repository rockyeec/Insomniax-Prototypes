using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : InputParent
{
    [SerializeField] private Button jumpButton = null;
    [SerializeField] private JoystickScript leftJoy = null;
    [SerializeField] private JoystickScript rightJoy = null;
    [SerializeField] private Camera3rdPerson cam = null;

    protected override void Init()
    {
        base.Init();
        jumpButton.onClick.AddListener(Jump);
    }

    protected override void Tick(float delta)
    {
        base.Tick(delta);

        cam.Tick(rightJoy.GetVerticalDelta(), rightJoy.GetHorizontalDelta(), transform);

        float hor = leftJoy.GetHorizontal();
        float ver = leftJoy.GetVertical();

        GetInputs().SmoothMoveInput(cam.GetRotation() * new Vector3(hor, 0.0f, ver), delta);

        
    }

    protected override void FixedTick(float delta)
    {
        base.FixedTick(delta);
    }

    protected override void LateTick(float delta)
    {
        base.LateTick(delta);
    }


    private void Jump()
    {
        GetInputs().jump = true;
    }
}
