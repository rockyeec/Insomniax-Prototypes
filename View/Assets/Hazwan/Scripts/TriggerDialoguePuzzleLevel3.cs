using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerDialoguePuzzleLevel3 : MonoBehaviour
{
    public static TriggerDialoguePuzzleLevel3 instance;

    bool isTriggered = false;

    bool isAbleToRun = true;

    [SerializeField] GameObject innerAlex = null;
    [SerializeField] GameObject fog = null;

    public Dialogue startDialogue;

    [SerializeField] GameObject wall = null;

    public static event Action OnRestart = delegate { };

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        OnRestart += TriggerDialoguePuzzleLevel3_OnRestart;
    }

    private void OnDestroy()
    {
        OnRestart -= TriggerDialoguePuzzleLevel3_OnRestart;
    }

    void Update()
    {
        if(isAbleToRun)
            DialoguePuzzleResult();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            if (startDialogue != null)
            {
                DialogueManager.instance.isDoneMCQL3 = false;
                DialogueManager.instance.StartDialogue(startDialogue);
                GameFeatures(false);
                isTriggered = true;
            }
        }
    }

    void DialoguePuzzleResult()
    {
        if (DialogueManager.instance.L3IsPass && DialogueManager.instance.isDoneMCQL3)
        {
            //Able to interact with tombstone
            StartCoroutine(DoneDialoguePuzzle(true));
            isAbleToRun = false;
            TombStoneScript.doneDialoguePuzzle = true;
        }
        else if (DialogueManager.instance.L3IsLose && DialogueManager.instance.isDoneMCQL3)
        {
            //Restart level
            StartCoroutine(DoneDialoguePuzzle(false));
            DialogueManager.instance.isDoneMCQL3 = false;
            OnRestart();
            isAbleToRun = false;
        }

    }

    public void RemoveFog()
    {
        StartCoroutine(DelayFog());
    }

    IEnumerator DelayFog()
    {
        yield return new WaitForSeconds(2f);
        wall.SetActive(false);
    }

    IEnumerator DoneDialoguePuzzle(bool isSolved)
    {
        if(isSolved)
        {
            yield return new WaitForSeconds(2f);
            GameFeatures(true);
            EntryPrompt.Instance.PromptActivation(11);
            //innerAlex.SetActive(false);
            //gameObject.SetActive(false);
            //fog.SetActive(false);
            BgPropAlign.Trigger();
        }
        else
        {
            yield return new WaitForSeconds(2f);
            MonologueScript.TriggerText(new string[2] {
                "Something is not right?",
                "Let's try again." });
            yield return new WaitForSeconds(4f);
            //GameFeatures(true);
            isTriggered = false;
            yield return new WaitForSeconds(2f);
            isAbleToRun = true;
            //wall.SetActive(true);
        }
    }

    void GameFeatures(bool enable)
    {
        if (enable)
        {
            InvokerForMonologue.Do("EnableCameraControl");
            InvokerForMonologue.Do("EnableJump");
            InvokerForMonologue.Do("EnableDiary");
            InvokerForMonologue.Do("EnableMoveControl");
        }
        else
        {
            InvokerForMonologue.Do("DisableCameraControl");
            InvokerForMonologue.Do("DisableJump");
            InvokerForMonologue.Do("DisableDiary");
            InvokerForMonologue.Do("DisableMoveControl");
        }
    }

    private void TriggerDialoguePuzzleLevel3_OnRestart()
    {
        DialogueManager.instance.isDoneMCQL3 = false;
        DialogueManager.instance.L3IsPass = false;
        DialogueManager.instance.L3IsLose = false;
        StartCoroutine(DoneDialoguePuzzle(false));
    }
}
