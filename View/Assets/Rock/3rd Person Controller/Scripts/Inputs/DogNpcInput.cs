using UnityEngine;

public class DogNpcInput : InputParent
{
    float duration = 0.69f;
    float time = 0.0f;
    private Vector3 direction;

    private void HandleNextAction()
    {
        if (Time.time >= time)
        {
            duration = UnityEngine.Random.Range(0.69f, 1.337f);
            time = Time.time + duration;

            direction.x = UnityEngine.Random.Range(-1.0f, 1.0f);
            direction.z = UnityEngine.Random.Range(-1.0f, 1.0f);
            direction.Normalize();

            Controller.inputs.jump = UnityEngine.Random.Range(0, 6) == 0;
        }
    }

    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddRegularTickBehavior(new ClimbBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());
    }

    protected override void Tick(in float delta)
    {
        base.Tick(delta);
        if (IsDisabled)
            return;

        HandleNextAction();

        // locomotion
        Controller.inputs.SmoothMoveInput(direction, delta);
    }
}
