using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiaryManager : MonoBehaviour
{
    protected bool isDiaryOpened = false;

    public GameObject nextBtn;
    public GameObject previousBtn;
    public GameObject DiaryContainer;

    [SerializeField] GameObject diaryContainerWithTag = null;
    [SerializeField] GameObject childContent = null;
    [SerializeField] GameObject promptEntry = null;
    [SerializeField] GameObject diaryPromptNotify = null;

    private static DiaryManager instance;
    private void Awake()
    {
        instance = this;
    }
    
    public static GameObject ChildContent { get { return instance.childContent; } }
    public static GameObject StaticDiaryContainer { get { return instance.diaryContainerWithTag; } }

    public static GameObject PromptEntry { get { return instance.promptEntry; } }
    public static GameObject DiaryPromptNotify { get { return instance.diaryPromptNotify; } }

    protected void ActiveDiary(GameObject diaryContainer)
    {
        if (diaryContainer.activeInHierarchy)
            isDiaryOpened = true;
        else
            isDiaryOpened = false;
    }

    protected void SetButton(bool nextButton, bool prevButton)
    {
        nextBtn.SetActive(nextButton);
        previousBtn.SetActive(prevButton);
    }

    public void ButtonsVisibility(int pageNum, List<GameObject> diaryList)
    {
        if (pageNum == 0)
            SetButton(true, false);
        else if (pageNum == diaryList.Count - 1)
            SetButton(false, true);
        else
            SetButton(true, true);
    }

    public void HiddenContent(int pageNum, List<GameObject> diaryContent)
    {
        for (int i = 0; i < diaryContent.Count; i++)
        {
            diaryContent[i].SetActive(false);
        }
        diaryContent[pageNum].SetActive(true);
    }
}
