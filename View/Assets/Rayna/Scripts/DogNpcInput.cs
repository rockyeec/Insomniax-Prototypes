using UnityEngine;

public class DogNpcInput : InputParent
{

    float time = 0.0f;
    Vector3 direction = Vector3.forward;

    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundBehavior());
        Controller.AddFixedTickBehavior(new LocomotionBehavior());
        Controller.AddFixedTickBehavior(new JumpBehavior());

        time = Time.time;
    }

    protected override void Tick(in float delta)
    {
        base.Tick(delta);
        if (IsDisabled)
            return;


        float dur = 0.5f;
        if (Time.time >= time)
        {
            time = Time.time + dur;
            direction = new Vector3(Random.Range(-0.2f, 0.2f), 0.0f, 0.5f);
            direction.Normalize();
        } 
        
        Controller.inputs.SmoothMoveInput(direction, delta);
    }
}
