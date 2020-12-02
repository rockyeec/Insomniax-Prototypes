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
        time = Time.time;

    }
    int barkInterval = 0;
    protected override void Tick(in float delta)
    {
        base.Tick(delta);
        if (IsDisabled)
            return;


        float dur = 0.5f;
        if (Time.time >= time)
        {
            time = Time.time + dur;
            if (barkInterval > 6)
            {
                barkInterval = 0;
                AudioManager.instance.PlaySfx("Bark");
            }
            else
            {
                ++barkInterval;
            }
            if (CalDist())
            {
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

        Controller.inputs.SmoothMoveInput(direction * 1.02f, delta);

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
