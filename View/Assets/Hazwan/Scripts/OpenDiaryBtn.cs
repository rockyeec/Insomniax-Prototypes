using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenDiaryBtn : MonoBehaviour
{
    public static OpenDiaryBtn Instance { get; private set; }
    private Button openDiaryBtn = null;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        openDiaryBtn = gameObject.GetComponent<Button>();

        openDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.OpenDiary();
        gameObject.SetActive(false);
    }

}
