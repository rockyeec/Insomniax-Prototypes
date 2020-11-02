using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDiaryBtn : MonoBehaviour
{
    private Button openDiaryBtn = null;

    void Start()
    {
        openDiaryBtn = gameObject.GetComponent<Button>();

        openDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.CloseDiary();
    }
}
