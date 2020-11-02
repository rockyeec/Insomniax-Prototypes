using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenDiaryBtn : MonoBehaviour
{
    private Button openDiaryBtn = null;

    void Start()
    {
        openDiaryBtn = gameObject.GetComponent<Button>();

        openDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.OpenDiary();
    }

}
