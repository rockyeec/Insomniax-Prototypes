﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior
{
    public abstract void Execute(CharacterController controller, in float delta);
}

