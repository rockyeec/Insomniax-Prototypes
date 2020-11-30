using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3StartTrigger : MonoBehaviour
{
    int isGlassesOn = 0;
    private void LateUpdate()
    {
        if (isGlassesOn == 0)
        {
            isGlassesOn = 1;            
        }
        else if (isGlassesOn == 1)
        {
            isGlassesOn = 2;
            //EntryPrompt.Instance.PromptActivation(8);
            InvokerForMonologue.Do("SetGlassesOn");
            InvokerForMonologue.Do("DisableGlasses");

            enabled = false;
        }
    }

}

