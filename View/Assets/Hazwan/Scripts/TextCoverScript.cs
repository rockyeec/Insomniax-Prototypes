using System.Collections.Generic;
using UnityEngine;

public class TextCoverScript : MonoBehaviour
{
    [SerializeField] int coverIndex = 0;
    [SerializeField] int diaryPage = 0;

    bool isTriggered = false;

    private ClickableObject clickable = null;
    private InteractableObserver observer = null;

    List<HologramHandler> hologrammables = new List<HologramHandler>();


    void Start()
    {
        InitHolo();

        CheckSavedData();
        clickable = gameObject.AddComponent<ClickableObject>();
        clickable.Init(this);
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.Init(coverIndex);        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            clickable.enabled = true;

            InteractPrompt.DoThing(transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck"))
        {
            clickable.enabled = false;

            InteractPrompt.UndoThing();
        }
    }

    public void DisableTextCover()
    {
        AudioManager.instance.PlaySfx("coverLayerRemoval");

        Diary.Instance.currentPage = diaryPage;
        Diary.Instance.ButtonsVisibility(diaryPage, Diary.DiaryList);
        Diary.Instance.HiddenContent(diaryPage, Diary.DiaryList);
        Diary.Instance.OpenButton.SetActive(false);
        Diary.Instance.DiaryEntry.SetActive(true);

        MakeSolid();

        DiaryManager.StaticDiaryContainer.transform.GetChild(0).gameObject.SetActive(true);
        DiaryManager.ChildContent.transform.GetChild(diaryPage).gameObject.SetActive(true);
        isTriggered = true;
        observer.Trigger();
    }

    void CheckSavedData()
    {
        string entryName = "Entry " + coverIndex.ToString();
        if (SaveSystem.GetBool(entryName))
        {
            MakeSolid();
            isTriggered = true;
        }
        else
        {
            MakeHolo();
        }
    }


    void InitHolo()
    {
        Renderer[] renderersArr = GetComponentsInChildren<Renderer>();
        foreach (var item in renderersArr)
        {
            HologramHandler holoHand = item.gameObject.AddComponent<HologramHandler>();
            hologrammables.Add(holoHand);
        }
    }
    void MakeSolid()
    {
        foreach (var item in hologrammables)
        {
            item.MakeSolid();
        }
    }
    void MakeHolo()
    {
        foreach (var item in hologrammables)
        {
            item.MakeHolo();
        }
    }
}
