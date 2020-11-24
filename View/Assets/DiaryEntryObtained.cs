using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntryObtained : MonoBehaviour
{
    bool isTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            EntryPrompt.Instance.PromptActivation(8);
            isTriggered = true;
        }
    }
}

