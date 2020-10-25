using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diary : DiaryManager
{
    static public List<string> diaryTextList = new List<string>();

    public GameObject nextBtn;
    public GameObject previousBtn;
    public GameObject DiaryContainer;

    public TextMeshProUGUI diaryTextPageLeft;
    public TextMeshProUGUI diaryTextPageRight;
    public string test;
    public string test2;
    public string test3;

    

    void Start()
    {
        previousBtn.SetActive(false);

        test = "Maggi goreng telur mata satu.";
        test2 = "Nasi putih + Tomyam kurang seafood.";
        test3 = "1 Lamb grill";

        diaryTextList.Add(test);
        diaryTextList.Add(test2);
        diaryTextList.Add(test3);
        //print(diaryTextList.Count);
        //print(test);

        diaryTextPageLeft.text = test;

        ActiveDiary(DiaryContainer);
    }

    public void ButtonVisibility(int pageNum)
    {
        if(pageNum == 0)
        {
            SetButton(true, false);
        }
        else if(pageNum == diaryTextList.Count - 1)
        {
            SetButton(false, true);
        }
        else
        {
            SetButton(true, true);
        }
    }

    public void NextPage()
    {
        currentPage++;
        ButtonVisibility(currentPage);
        diaryTextPageLeft.text = diaryTextList[currentPage];
    }

    public void PreviousPage()
    {
        currentPage--;
        ButtonVisibility(currentPage);
        diaryTextPageLeft.text = diaryTextList[currentPage];
    }


    void SetButton(bool nextButton, bool prevButton)
    {
        nextBtn.SetActive(nextButton);
        previousBtn.SetActive(prevButton);
    }

    public void DiaryVisibility(GameObject diaryContainer)
    {
        currentPage = 0;
        ButtonVisibility(currentPage);
        diaryTextPageLeft.text = diaryTextList[currentPage];
        isDiaryOpened = !isDiaryOpened;
        diaryContainer.SetActive(isDiaryOpened);
    }

}
