using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOriginalForCharacter : BackToOriginal
{
    InputParent inputParent;
    override protected void Start()
    {
        base.Start();
        inputParent = GetComponent<InputParent>();
    }
    protected override void OnStartLerp()
    {
        base.OnStartLerp();
        inputParent.DisableController();
    }
    protected override void OnEndLerp()
    {
        base.OnEndLerp();
        inputParent.EnableController();
    }
}
