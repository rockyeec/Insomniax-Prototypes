using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior
{
    public abstract void Execute(CharacterController controller, float delta);
}

public abstract class FixedTickBehavior : Behavior { }
public abstract class RegularTickBehavior : Behavior { }

