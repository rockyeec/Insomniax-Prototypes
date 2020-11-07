using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDiaryBtn : MonoBehaviour
{
    private Button closeDiaryBtn = null;

    [SerializeField]
    private GameObject openDiaryBtn = null;

    void Start()
    {
        closeDiaryBtn = gameObject.GetComponent<Button>();

        closeDiaryBtn.onClick.AddListener(CallDiaryFunc);
    }

    public void CallDiaryFunc()
    {
        Diary.Instance.CloseDiary();
        openDiaryBtn.SetActive(true);
    }
}
