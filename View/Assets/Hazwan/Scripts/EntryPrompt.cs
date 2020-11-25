using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPrompt : MonoBehaviour
{
    //[SerializeField] int coverIndex = 0;

    private InteractableObserver observer = null;

    public static EntryPrompt Instance;

    public GameObject panel;

    //public GameObject notifyPanel;

    bool isTriggered = false;

    Vector3 originScale;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originScale = new Vector3(0.2565746f, 0.1344579f, 0.2565746f);
        //notifyPanel.SetActive(false);
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
        print("Play_1");
        yield return new WaitForSeconds(1.5f);
        //notifyPanel.SetActive(true);
        print("Play_2");
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        print("Play_3");
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(false);
    }

    void CheckSavedData(int index, string entryName)
    {
        if (SaveSystem.GetBool(entryName))
        {
            print("SAVED");
        }
        else
        {
            print("NONE");
            
            observer.Init(index);
            StartCoroutine(DelaySetActiveFalse());

            observer.TriggerDiaryPrompt();
            SaveSystem.SetBool(entryName, false);
            isTriggered = true;
            SaveSystem.SetBool(entryName, true);
            
        }
    }

}
