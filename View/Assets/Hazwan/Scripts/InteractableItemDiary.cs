using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemDiary : MonoBehaviour
{
    [SerializeField]
    private string desc = null;

    bool isTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            Diary.diaryTextList.Add(desc);
            isTriggered = true;
            print(Diary.diaryTextList.Count);
        }
    }
}
