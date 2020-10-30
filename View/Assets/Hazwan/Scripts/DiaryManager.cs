using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryManager : MonoBehaviour
{
    protected bool isDiaryOpened = false;
    protected int currentPage = 0;

    public GameObject nextBtn;
    public GameObject previousBtn;
    public GameObject DiaryContainer;

    public TextMeshProUGUI diaryTextPageLeft;
    public TextMeshProUGUI diaryTextPageRight;

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

    protected void OpenDiary()
    {
        DiaryContainer.SetActive(true);
    }

    public void ButtonsVisibility(int pageNum, List<string> diaryList)
    {
        if (pageNum == 0)
        {
            SetButton(true, false);
        }
        else if (pageNum == diaryList.Count - 1)
        {
            SetButton(false, true);
        }
        else
        {
            SetButton(true, true);
        }
    }
}
