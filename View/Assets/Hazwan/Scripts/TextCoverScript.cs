using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextCoverScript : MonoBehaviour
{
    [SerializeField] int coverIndex = 0;

    private GameObject diaryContainer;
    private GameObject childContent;

    private int diaryEntryNum = 0;
    bool isTriggered = false;

    private ClickableObject clickable = null;
    private InteractableObserver observer = null;

    private void Start()
    {
        CheckSavedData();
        clickable = gameObject.AddComponent<ClickableObject>();
        clickable.Init(this);
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.Init(coverIndex);

        diaryContainer = GameObject.FindGameObjectWithTag("DiaryPackage");
        childContent = GameObject.FindGameObjectWithTag("DiaryContent");
    }

    void CheckSavedData()
    {
        string entryName = "Entry " + coverIndex.ToString();
        if (SaveSystem.GetBool(entryName))
        {
            isTriggered = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            clickable.enabled = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck"))
        {
            clickable.enabled = false;
        }
    }

    public void DisableTextCover()
    {
        Diary.Instance.currentPage = diaryEntryNum;
        Diary.Instance.ButtonsVisibility(diaryEntryNum, Diary.diaryContent);
        Diary.Instance.OpenButton.SetActive(false);
        Diary.Instance.DiaryEntry.SetActive(true);
        diaryContainer.transform.GetChild(0).gameObject.SetActive(true);
        childContent.transform.GetChild(coverIndex).gameObject.SetActive(true);
        isTriggered = true;
        observer.Trigger();
    }


}
