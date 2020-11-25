using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3StartTrigger : MonoBehaviour
{
    private void Start()
    {
        //EntryPrompt.Instance.PromptActivation(8);
        InvokerForMonologue.Do("SetGlassesOn");
    }

}

