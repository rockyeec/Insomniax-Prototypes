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

    static public List<GameObject> diaryList = new List<GameObject>();

    public int currentPage = 0;

    public GameObject[] diaryContent;

    public GameObject ContentCanvas;
    public GameObject DiaryEntry;
    public GameObject OpenButton;

    public GameObject coverLayer_1;

    void Start()
    {
        for (int i = 0; i < diaryContent.Length; i++)
        {
            diaryList.Add(diaryContent[i]);
        }
        previousBtn.SetActive(false);
        ActiveDiary(DiaryContainer);
    }

    public void NextPage()
    {
        AudioManager.instance.Play("FlipBook", "SFX");
        currentPage++;
        ButtonsVisibility(currentPage, diaryList);
        HiddenContent(currentPage,diaryList);
    }

    public void PreviousPage()
    {
        AudioManager.instance.Play("FlipBook", "SFX");
        currentPage--;
        ButtonsVisibility(currentPage, diaryList);
        HiddenContent(currentPage, diaryList);
    }

    public void OpenDiary()
    {
        OpenButton.SetActive(false);
        DiaryContainer.SetActive(true);
        diaryList[0].SetActive(true);
        ContentCanvas.SetActive(true);
        DiaryEntry.SetActive(true);

        InvokerForMonologue.IsHold = false;
    }

    public void CloseDiary()
    {
        OpenButton.SetActive(true);
        currentPage = 0;
        ButtonsVisibility(currentPage, diaryList);
        HiddenContent(currentPage, diaryList);
        DiaryContainer.SetActive(false);
        ContentCanvas.SetActive(false);
        DiaryEntry.SetActive(false);

        InvokerForMonologue.IsHold = false;
    }
   
}
