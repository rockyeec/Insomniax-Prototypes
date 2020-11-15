using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue startDialogue;

    public Letter letterObj;

    private bool doneOn = false;

    // Start is called before the first frame update
    void Start()
    {
        if(startDialogue != null)
        {
            DialogueManager.instance.StartDialogue(startDialogue);
        }
    }

    private void Update()
    {
        if(DialogueManager.instance.isDoneMCQL3 == true)
        {
            if (letterObj != null && doneOn == false)
            {
                letterObj.PanelOnOfF(true);
                doneOn = true;
            }
        }
    }

}
