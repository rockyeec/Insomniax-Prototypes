using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3StartTrigger : MonoBehaviour
{
    bool isGlassesOn = false;
    private void Update()
    {
        if (isGlassesOn)
            return;

        isGlassesOn = true;

        //EntryPrompt.Instance.PromptActivation(8);
        InvokerForMonologue.Do("SetGlassesOn");
    }

}

