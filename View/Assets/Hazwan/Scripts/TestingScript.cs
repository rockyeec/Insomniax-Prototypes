using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public Dialogue startDialogue;
    void Start()
    {
        DialogueManager.instance.StartDialogue(startDialogue);
    }

    void Update()
    {
        
    }
}
