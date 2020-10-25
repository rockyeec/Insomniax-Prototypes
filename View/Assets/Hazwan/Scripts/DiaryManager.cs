using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryManager : MonoBehaviour
{
    protected bool isDiaryOpened = false;
    protected int currentPage = 0;


    protected void ActiveDiary(GameObject diaryContainer)
    {
        if (diaryContainer.activeInHierarchy)
            isDiaryOpened = true;
        else
            isDiaryOpened = false;

        print(diaryContainer.activeInHierarchy);
    }

    
}
