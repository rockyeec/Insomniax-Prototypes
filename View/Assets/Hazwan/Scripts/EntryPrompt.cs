using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPrompt : MonoBehaviour
{
    //[SerializeField] int coverIndex = 0;

    private InteractableObserver observer = null;

    public static EntryPrompt Instance;

    public void Awake()
    {
        Instance = this;
    }
    public void PromptActivation(int coverLayerNum) // <- Call this func
    {
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.Init(coverLayerNum);
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(true);
        observer.Trigger();
        StartCoroutine(DelaySetActiveFalse(2f));
    }

    IEnumerator DelaySetActiveFalse(float duration)
    {
        yield return new WaitForSeconds(duration);
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(false);
    }

}
