using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    public CustomInputs inputs = new CustomInputs();
    public class CustomInputs
    {
        public Vector3 MoveDir { get; private set; }

        public void SmoothMoveInput(Vector3 value, float delta)
        {
            MoveDir = Vector3.MoveTowards(MoveDir, value, delta * 6.9f);
        }

        public bool jump = false;
        public bool jumpRelease = false;
    }

    public CustomOutputs outputs = new CustomOutputs();
    public class CustomOutputs
    {
        public bool onGround = false;
        public float vertical = 0.0f;
        public float horizontal = 0.0f;
        public float deltaRot = 0.0f;
        public bool animateJump = false;
    }

    public Transform CharTransform { get; private set; }
    public Rigidbody Rb { get; private set; }
    public bool Hold { get; set; }

    readonly private List<FixedTickBehavior> fixedTicks = new List<FixedTickBehavior>();
    readonly private List<RegularTickBehavior> regularTicks = new List<RegularTickBehavior>();
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
        Rb = rb;
        CharTransform = rb.transform;
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

    /*public void LateTick(float delta)
    {
    }*/
}
