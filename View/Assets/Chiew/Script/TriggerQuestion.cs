using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestion : MonoBehaviour
{
    public Dialogue FirstQuestion;

    private bool isStartedLevel = false;

    void Start()
    {
        GameScript.OnPause += GameScript_Paused;
        GameScript.OnUnpause += GameScript_Unpaused;
    }

    private void GameScript_Paused()
    {
        //if pause
        //Still thinking about this
    }

    private void GameScript_Unpaused()
    {
        //If unpause
        //Currently start this
        DialogueManager.instance.StartDialogue(FirstQuestion);
        isStartedLevel = true;
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_Paused;
        GameScript.OnUnpause -= GameScript_Unpaused;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isStartedLevel == true)
        {
            if (DialogueManager.instance.isDialogueDone == true)
            {
                //Switch next level
                Debug.Log("Next level");
                LevelManager.LoadNextLevel();
                isStartedLevel = false;
            }
        }
    }
}
