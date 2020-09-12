using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    public CustomInputs inputs = new CustomInputs();
    public class CustomInputs
    {
        public Vector3 moveDir { get; private set; }

        public void SmoothMoveInput(Vector3 value, float delta)
        {
            moveDir = Vector3.MoveTowards(moveDir, value, delta * 6.9f);
        }

        public bool jump = false;
    }

    public CustomOutputs outputs = new CustomOutputs();
    public class CustomOutputs
    {
        public bool onGround = false;
        public float vertical = 0.0f;
        public float horizontal = 0.0f;
        public float deltaRot = 0.0f;
        public bool jump = false;
    }

    public Transform transform { get; private set; }
    public Rigidbody rb { get; private set; }
    public bool hold { get; set; }

    private List<FixedTickBehavior> fixedTicks = new List<FixedTickBehavior>();
    private List<RegularTickBehavior> regularTicks = new List<RegularTickBehavior>();
    public void AddFixedTickBehavior(FixedTickBehavior behavior)
    {
        fixedTicks.Add(behavior);
    }
    public void AddRegularTickBehavior(RegularTickBehavior behavior)
    {
        regularTicks.Add(behavior);
    }


    public CharacterController(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        this.rb = rb;
        transform = rb.transform;
    }

    public void Tick(float delta)
    {
        foreach (var item in regularTicks)
        {
            item.Execute(this, delta);
        }
    }

    public void FixedTick(float delta)
    {
        foreach (var item in fixedTicks)
        {
            item.Execute(this, delta);
        }
    }

    public void LateTick(float delta)
    {
    }
}
