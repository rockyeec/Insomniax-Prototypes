using System.Collections;
using UnityEngine;
using TMPro;

public class InteractableSubject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] coverArr = null;

    //public TileGame tileGame = new TileGame();

    private void Awake()
    {
        //tileGame.Init();
        InteractableObserver.OnInteracted += InteractableObserver_OnInteracted;
        InteractableObserver.OnInteractedDiaryPrompt += InteractableObserver_OnInteractedDiaryPrompt;
        InteractableObserver.OnInteractedCheckDiaryPrompt += InteractableObserver_OnInteractedCheckDiaryPrompt;

        for (int i = 0; i < coverArr.Length; i++)
        {
            string entryName = "Entry " + i.ToString();
            if (SaveSystem.GetBool(entryName))
            {
                coverArr[i].gameObject.SetActive(false);
            }
        }
    }

    

    private void OnDestroy()
    {
        InteractableObserver.OnInteracted -= InteractableObserver_OnInteracted;
        InteractableObserver.OnInteractedDiaryPrompt -= InteractableObserver_OnInteractedDiaryPrompt;
        InteractableObserver.OnInteractedCheckDiaryPrompt -= InteractableObserver_OnInteractedCheckDiaryPrompt;
    }

    private void InteractableObserver_OnInteracted(int i)
    {
        SaveSystem.SetBool("Entry " + i.ToString(), true);
        //tileGame.SavedArray(i);
        float duration = 2.0f;
        coverArr[i].CrossFadeAlpha(0.0f, duration, true);
        StartCoroutine(DelaySetActiveFalse(i, duration));
    }

    IEnumerator DelaySetActiveFalse(int i, float duration)
    {
        yield return new WaitForSeconds(duration);
        coverArr[i].gameObject.SetActive(false);
    }

    private void InteractableObserver_OnInteractedDiaryPrompt(int a)
    {
        
        DisableCoverLayerForDiaryPrompt(a);
    }

    void DisableCoverLayerForDiaryPrompt(int i)
    {
        coverArr[i].gameObject.SetActive(false);
    }

    private void InteractableObserver_OnInteractedCheckDiaryPrompt()
    {
        for (int i = 6; i < coverArr.Length; i++)
        {
            string entryName = "DiaryPrompt " + i.ToString();
            if (SaveSystem.GetBool(entryName))
            {
                coverArr[i].gameObject.SetActive(false);
            }
        }
    }


}
