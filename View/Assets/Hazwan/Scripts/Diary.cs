using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : DiaryManager
{
    #region Singleton

    private static Diary _instance;

    public static Diary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Diary>();
            }
            return _instance;
        }
    }
    #endregion

    static public List<GameObject> diaryContent = new List<GameObject>();


    public GameObject firstContent;
    public GameObject secondContent;
    public GameObject thirdContent;

    public GameObject ContentCanvas;
    public GameObject DiaryEntry;

    void Start()
    {
        previousBtn.SetActive(false);
        diaryContent.Add(firstContent);
        diaryContent.Add(secondContent);
        diaryContent.Add(thirdContent);
        //print(diaryContent.Count);
        ActiveDiary(DiaryContainer);
    }

    public void NextPage()
    {
        currentPage++;
        ButtonsVisibility(currentPage, diaryContent);
        HiddenContent(currentPage,diaryContent);
    }

    public void PreviousPage()
    {
        currentPage--;
        ButtonsVisibility(currentPage, diaryContent);
        HiddenContent(currentPage, diaryContent);
    }

    public void OpenDiary()
    {
        DiaryContainer.SetActive(true);
        diaryContent[0].SetActive(true);
        ContentCanvas.SetActive(true);
        DiaryEntry.SetActive(true);
    }

    public void CloseDiary()
    {
        currentPage = 0;
        ButtonsVisibility(currentPage, diaryContent);
        HiddenContent(currentPage, diaryContent);
        DiaryContainer.SetActive(false);
        ContentCanvas.SetActive(false);
        DiaryEntry.SetActive(false);
    }
   
}
