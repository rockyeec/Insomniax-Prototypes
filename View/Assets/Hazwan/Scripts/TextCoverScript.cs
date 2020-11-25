using System.Collections.Generic;
using UnityEngine;

public class TextCoverScript : MonoBehaviour
{
    [SerializeField] int coverIndex = 0;
    [SerializeField] int diaryPage = 0;

    bool isTriggered = false;

    private ClickableObject clickable = null;
    private InteractableObserver observer = null;

    MeshRenderer callTempMeshRend;

    Material[] matArrayOutline;
    Material[] matArrayNormal;

    void Start()
    {
        Outline();
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
        Diary.Instance.currentPage = diaryPage;
        Diary.Instance.ButtonsVisibility(diaryPage, Diary.DiaryList);
        Diary.Instance.HiddenContent(diaryPage, Diary.DiaryList);
        Diary.Instance.OpenButton.SetActive(false);
        Diary.Instance.DiaryEntry.SetActive(true);
        callTempMeshRend.materials = matArrayNormal;
        DiaryManager.StaticDiaryContainer.transform.GetChild(0).gameObject.SetActive(true);
        DiaryManager.ChildContent.transform.GetChild(diaryPage).gameObject.SetActive(true);
        isTriggered = true;
        observer.Trigger();
    }

    public void Outline()
    {
        callTempMeshRend = GetComponentInChildren<MeshRenderer>();
        matArrayNormal = callTempMeshRend.materials;
        List<Material> listMaterial = new List<Material>();
        listMaterial.AddRange(matArrayNormal);
        listMaterial.Add(MaterialManager.OutLineMaterial);
        matArrayOutline = listMaterial.ToArray();
    }

    void CheckSavedData()
    {
        string entryName = "Entry " + coverIndex.ToString();
        if (SaveSystem.GetBool(entryName))
        {
            callTempMeshRend.materials = matArrayNormal;
            isTriggered = true;
        }
        else
        {
            callTempMeshRend.materials = matArrayOutline;
        }
    }
}
