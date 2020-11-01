using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableItemDiaryContent : MonoBehaviour
{
    [SerializeField]
    private GameObject content = null;

    bool isTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            Diary.diaryContent.Add(content);
            isTriggered = true;
            gameObject.SetActive(false);
            print(Diary.diaryContent.Count);
        }
    }
}
