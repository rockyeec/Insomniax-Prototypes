using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public static List<GameObject> DiaryList { get { return Instance.diaryList; } }

    public List<GameObject> diaryList = new List<GameObject>();

    public int currentPage = 0;

    public GameObject[] diaryContent;

    public GameObject ContentCanvas;
    public GameObject DiaryEntry;
    public GameObject OpenButton;

    public LeanTweenType type;

    Vector3 diaryDefaultPos;
    Vector3 entryDefaultPos;
    Vector3 resetPos;
    Vector3 diaryOriScale = new Vector3(0.7131789f, 0.8681303f, 1);
    Vector3 entryOriScale = new Vector3(1.141086f, 1.389008f, 1.6f);

    void Start()
    {
        for (int i = 0; i < diaryContent.Length; i++)
        {
            diaryList.Add(diaryContent[i]);
        }
        previousBtn.SetActive(false);
        ActiveDiary(DiaryContainer);
        diaryDefaultPos = DiaryContainer.transform.position;
        entryDefaultPos = diaryContent[0].transform.position;
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
        AudioManager.instance.Play("OpenBook", "SFX");
        OpenButton.SetActive(false);
        DiaryContainer.SetActive(true);
        diaryList[0].SetActive(true);
        ContentCanvas.SetActive(true);
        DiaryEntry.SetActive(true);
        PlayAnim(DiaryContainer, diaryDefaultPos, diaryOriScale);
        PlayAnim(diaryList[0], entryDefaultPos, entryOriScale);
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

    public void PlayAnim(GameObject go, Vector3 position, Vector3 scale)
    {
        ResetAnim(go);
        LeanTween.move(go, position, 0.3f).setEaseLinear();
        LeanTween.scale(go, scale, 0.3f).setEaseLinear();
    }

    public void ResetAnim(GameObject go)
    {
        resetPos = new Vector3(-30f, diaryDefaultPos.y + 200f, 0);
        go.transform.position = resetPos;
        go.transform.localScale = Vector3.zero;
    }
   
}
