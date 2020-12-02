using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPrompt : MonoBehaviour
{
    private InteractableObserver observer = null;

    public static EntryPrompt Instance;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DiaryManager.PromptEntry.transform.gameObject.SetActive(false);
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.TriggerCheckDiaryPrompt();
    }

    public void PromptActivation(int coverLayerNum)
    {
        string entryName = "DiaryPrompt " + coverLayerNum.ToString();
        
        CheckSavedData(coverLayerNum, entryName);
    }

    IEnumerator DelaySetActiveFalse()
    {
        InvokerForMonologue.Do("DisableMenu");
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySfx("diaryTrigger");
        DiaryManager.DiaryPromptNotify.gameObject.SetActive(true);
        DiaryManager.PromptEntry.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        DiaryManager.PromptEntry.gameObject.SetActive(false);
        InvokerForMonologue.Do("EnableMenu");
    }

    void CheckSavedData(int index, string entryName)
    {
        if (SaveSystem.GetBool(entryName))
        {
            //print("SAVED");
        }
        else
        {
            //print("NONE");
            observer.Init(index);
            StartCoroutine(DelaySetActiveFalse());

            observer.TriggerDiaryPrompt();
            SaveSystem.SetBool(entryName, true);
            
        }
    }
}
