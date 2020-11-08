using System.Collections;
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

    private int diaryContentChildNum = 0;

    string diaryContentTag;
    string diaryTag;

    private float timeDelay = 2;

    bool isTriggered = false;

    public bool coverLayer1 = false;
    public bool coverLayer2 = false;

    private void Start()
    {
        diaryContentTag = "DiaryContent";
        diaryTag = "DiaryPackage";
        GetTag();
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
        StartCoroutine(DelayFadeAnim());
    }

    IEnumerator DelayFadeAnim()
    {
        diaryContainer = GameObject.FindGameObjectWithTag(diaryTag);
        childContent = GameObject.FindGameObjectWithTag(diaryContentTag);
        diaryContainer.transform.GetChild(diaryContentChildNum).gameObject.SetActive(true);
        //childContent.transform.GetChild(diaryContentChildNum).gameObject.SetActive(true);
        childContent.transform.GetChild(0).gameObject.SetActive(true);
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
            diaryContentChildNum = 0;
        }
        else if (coverLayer2) //Not set yet
        {
            //textCoverName = "TextCovered2";
            //diaryContentChildNum = 1;
        }
    }
}
