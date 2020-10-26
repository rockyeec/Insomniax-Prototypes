using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Diary : DiaryManager
{
    static public List<string> diaryTextList = new List<string>();

    public string test;
    public string test2;
    public string test3;

    void Start()
    {
        previousBtn.SetActive(false);

        test = "Maggi goreng telur mata satu.";
        test2 = "Nasi putih + Tomyam seafood.";
        test3 = "1 Lamb grill";

        diaryTextList.Add(test);
        diaryTextList.Add(test2);
        diaryTextList.Add(test3);

        diaryTextPageLeft.text = test;

        ActiveDiary(DiaryContainer);
    }

    public void NextPage()
    {
        currentPage++;
        ButtonsVisibility(currentPage, diaryTextList);
        diaryTextPageLeft.text = diaryTextList[currentPage];
    }

    public void PreviousPage()
    {
        currentPage--;
        ButtonsVisibility(currentPage, diaryTextList);
        diaryTextPageLeft.text = diaryTextList[currentPage];
    }

    public void CloseDiary()
    {
        currentPage = 0;
        ButtonsVisibility(currentPage, diaryTextList);
        diaryTextPageLeft.text = diaryTextList[currentPage];
        DiaryContainer.SetActive(false);
    }

}
