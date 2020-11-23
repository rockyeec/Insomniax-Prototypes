using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Triggers : MonoBehaviour
{
    private void Start()
    {
        EntryPrompt.Instance.PromptActivation(6);
    }
}
