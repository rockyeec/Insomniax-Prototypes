using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPrompt : MonoBehaviour
{
    //[SerializeField] int coverIndex = 0;

    private InteractableObserver observer = null;

    public static EntryPrompt Instance;

    public GameObject panel;

    public GameObject notifyPanel;

    Vector3 originScale;

    public void Awake()
    {
        originScale = new Vector3(0.2565746f, 0.1344579f, 0.2565746f);
        Instance = this;
        //panel.transform.localScale = Vector3.zero;
    }
    public void PromptActivation(int coverLayerNum) // <- Call this func
    {
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.Init(coverLayerNum);
        StartCoroutine(DelaySetActiveFalse());
        
        observer.Trigger();
    }

    IEnumerator DelaySetActiveFalse()
    {
        print("Play_1");
        yield return new WaitForSeconds(1.5f);
        notifyPanel.SetActive(true);
        print("Play_2");
        //LeanTween.scale(panel, originScale, 3f);
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        print("Play_3");
        //LeanTween.scale(panel, Vector3.zero, 3f);
        DiaryManager.PromptEntry.transform.GetChild(0).gameObject.SetActive(false);
    }

}
