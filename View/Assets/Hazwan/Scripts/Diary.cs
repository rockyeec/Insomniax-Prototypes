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

    public int currentPage = 0;

    public GameObject firstContent;
    public GameObject secondContent;
    public GameObject thirdContent;
    public GameObject fourthContent;
    public GameObject fifthContent;

    public GameObject ContentCanvas;
    public GameObject DiaryEntry;
    public GameObject OpenButton;

    public GameObject coverLayer_1;

    void Start()
    {
        previousBtn.SetActive(false);
        diaryContent.Add(firstContent);
        diaryContent.Add(secondContent);
        diaryContent.Add(thirdContent);
        diaryContent.Add(fourthContent);
        diaryContent.Add(fifthContent);
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
        OpenButton.SetActive(false);
        DiaryContainer.SetActive(true);
        diaryContent[0].SetActive(true);
        ContentCanvas.SetActive(true);
        DiaryEntry.SetActive(true);

        InvokerForMonologue.IsHold = false;
    }

    public void CloseDiary()
    {
        OpenButton.SetActive(true);
        currentPage = 0;
        ButtonsVisibility(currentPage, diaryContent);
        HiddenContent(currentPage, diaryContent);
        DiaryContainer.SetActive(false);
        ContentCanvas.SetActive(false);
        DiaryEntry.SetActive(false);

        InvokerForMonologue.IsHold = false;
    }
   
}
