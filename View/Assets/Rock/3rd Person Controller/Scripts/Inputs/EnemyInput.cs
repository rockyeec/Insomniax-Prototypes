using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : InputParent
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
            direction *= 0.420f;
        }
    }

    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        //Controller.AddFixedTickBehavior(new JumpBehavior());
        Controller.AddRegularTickBehavior(new ClimbBehavior());
    }

    protected override void Tick(in float delta)
    {
        base.Tick(delta);

        HandleNextAction();

        // locomotion
        Controller.inputs.SmoothMoveInput(direction, delta);

        // jump
        /*if (jumpButton.IsDown)
        {
            Controller.inputs.jump = true;
        }

        if (jumpButton.IsUp)
        {
            Controller.inputs.jumpRelease = true;
        }*/
    }

    protected override void FixedTick(in float delta)
    {
        base.FixedTick(delta);
    }

    protected override void LateTick(in float delta)
    {
        base.LateTick(delta);
    }
}
