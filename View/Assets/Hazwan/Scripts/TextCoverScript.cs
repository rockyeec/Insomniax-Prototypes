﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCoverScript : MonoBehaviour
{
    #region Singleton

    private static TextCoverScript _instance;

    public static TextCoverScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TextCoverScript>();
            }
            return _instance;
        }
    }
    #endregion

    private GameObject diaryContainer;
    private GameObject childContent;
    GameObject textCovered;

    private string darkenTextTag;

    private int diaryEntryNum = 0;

    string diaryContentTag;
    string diaryTag;

    private float timeDelay = 2;

    bool isTriggered = false;

    public bool coverLayer1 = false;
    public bool coverLayer2 = false;

    private string saveEntryID;

    private void Start()
    {
        diaryContentTag = "DiaryContent";
        diaryTag = "DiaryPackage";
        diaryContainer = GameObject.FindGameObjectWithTag("DiaryPackage"); // Don't change this [Note to self] - Diary Container
        childContent = GameObject.FindGameObjectWithTag("DiaryContent"); // Don't change this [Note to self] - Diary Entries
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            isTriggered = true;
            
            DisableTextCover();
        }
    }

    public void DisableTextCover()
    {
        Diary.Instance.OpenButton.SetActive(false);
        Diary.Instance.DiaryEntry.SetActive(true);
        StartCoroutine(DelayFadeAnim());
    }

    IEnumerator DelayFadeAnim()
    {
        diaryContainer.transform.GetChild(0).gameObject.SetActive(true); // Don't change this [Note to self]

        //childContent.transform.GetChild(diaryContentChildNum).gameObject.SetActive(true);
        GetTag();

        childContent.transform.GetChild(diaryEntryNum).gameObject.SetActive(true);
        textCovered = GameObject.FindGameObjectWithTag(darkenTextTag);

        yield return new WaitForSeconds(1.5f);
        TextMeshProUGUI temp = textCovered.GetComponent<TextMeshProUGUI>();
        FadeOutDarkenText(temp);

        yield return new WaitForSeconds(timeDelay);
        textCovered.SetActive(false);
    }

    void FadeOutDarkenText(TextMeshProUGUI textCovered)
    {
        textCovered.CrossFadeAlpha(0, timeDelay, true);
    }

    void GetTag()
    {
        if (coverLayer1)
        {
            darkenTextTag = "TextCovered1";
            diaryEntryNum = 0;
            saveEntryID = "entryOne";
        }
        else if (coverLayer2)
        {
            darkenTextTag = "TextCovered2";
            diaryEntryNum = 1;
            saveEntryID = "entryTwo";
        }

        Diary.Instance.currentPage = diaryEntryNum;
        Diary.Instance.ButtonsVisibility(diaryEntryNum, Diary.diaryContent);
    }
}
