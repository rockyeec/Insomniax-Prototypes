using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryEntryObtained : MonoBehaviour
{
    private void Start()
    {
        EntryPrompt.Instance.PromptActivation(8);
    }

}

