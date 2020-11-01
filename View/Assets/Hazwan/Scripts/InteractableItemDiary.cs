using UnityEngine;
using TMPro;

public class InteractableItemDiary : MonoBehaviour
{
    [Header("Text Cover Layer")]
    [SerializeField]
    private bool isCoverText = false;
    [SerializeField]
    private TextMeshProUGUI textCover;

    [Header("Diary Content")]
    [SerializeField]
    private GameObject content = null;

    bool isTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered && !isCoverText)
        {
            Diary.diaryContent.Add(content);
            isTriggered = true;
            gameObject.SetActive(false);
            print(Diary.diaryContent.Count);
        } 
        
        if(col.gameObject.CompareTag("TriggerCheck") && isCoverText && !isTriggered)
        {
            isTriggered = true;
            gameObject.SetActive(false);
            TextCoverScript.Instance.DisableTextCover(textCover);
        }
    }

    //3rd Person Player Package/Canvas/Diary Package/DiaryContent/Content - 1/PageText - LowerLeft/Covered
}
