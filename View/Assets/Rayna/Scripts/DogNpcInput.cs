using UnityEngine;

public class DogNpcInput : InputParent
{
    [SerializeField] float xLimits = 3.0f;
    public GameObject player;
    float time = 0.0f;
    Vector3 direction = Vector3.forward;

    protected override void Init()
    {
        base.Init();

        Controller.AddFixedTickBehavior(new CheckGroundWithNoMovingPlatformBehavior());
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
            if (CalDist())
            {
                time = Time.time + dur;
                direction = new Vector3(Random.Range(-0.2f, 0.2f), 0.0f, 0.5f);
                direction.Normalize();
            }
            else
            {
                direction = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        if (transform.position.x > xLimits)
        {
            direction.x = -Mathf.Abs( direction.x);
        }
        else if (transform.position.x < -xLimits)
        {
            direction.x = Mathf.Abs(direction.x);
        }

        Controller.inputs.SmoothMoveInput(direction, delta);

    }
    public bool CalDist()
    {
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance <= 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
