using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrevDiaryBtn : MonoBehaviour
{
    void Start()
    {
        Button prevDiaryBtn = gameObject.GetComponent<Button>();

        prevDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.PreviousPage();
    }
}
