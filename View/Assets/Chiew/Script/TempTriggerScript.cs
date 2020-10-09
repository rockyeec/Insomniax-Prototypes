using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTriggerScript : MonoBehaviour
{
    public Dialogue d1;

    void Start()
    {
        GameScript.OnPause += GameScript_Paused;
        GameScript.OnUnpause += GameScript_Unpaused;
    }

    private void GameScript_Paused()
    {

    }

    private void GameScript_Unpaused()
    {
        DialogueManager.instance.StartDialogue(d1);
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_Paused;
        GameScript.OnUnpause -= GameScript_Unpaused;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
