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

    public GameObject DiaryContainer;
    public GameObject TextCover;

    private float timeDelay = 2;

    public void DisableTextCover(TextMeshProUGUI textCovered)
    {
        StartCoroutine(DelayFadeAnim(textCovered));
    }

    IEnumerator DelayFadeAnim(TextMeshProUGUI textCovered)
    {
        DiaryContainer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FadeOutTextCover(textCovered);
        yield return new WaitForSeconds(timeDelay);
        TextCover.SetActive(false);
    }

    void FadeOutTextCover(TextMeshProUGUI textCovered)
    {
        textCovered.CrossFadeAlpha(0, timeDelay, true);
    }
}
