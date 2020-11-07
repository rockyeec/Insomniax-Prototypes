using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDiaryBtn : MonoBehaviour
{
    void Start()
    {
        Button nextDiaryBtn = gameObject.GetComponent<Button>();

        nextDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.NextPage();
    }
}
