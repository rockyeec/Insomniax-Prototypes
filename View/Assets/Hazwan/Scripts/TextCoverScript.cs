using System.Collections.Generic;
using UnityEngine;

public class TextCoverScript : MonoBehaviour
{
    [SerializeField] int coverIndex = 0;
    [SerializeField] int diaryPage = 0;

    private GameObject diaryContainer;
    private GameObject childContent;

    bool isTriggered = false;

    private ClickableObject clickable = null;
    private InteractableObserver observer = null;

    MeshRenderer callTempMeshRend;

    Material[] matArrayOutline;
    Material[] matArrayNormal;

    void Start()
    {
        CheckSavedData();
        clickable = gameObject.AddComponent<ClickableObject>();
        clickable.ObjectName.Add(gameObject);
        clickable.Init(this);
        observer = gameObject.AddComponent<InteractableObserver>();
        observer.Init(coverIndex);
        Outline();
        diaryContainer = GameObject.FindGameObjectWithTag("DiaryPackage");
        childContent = GameObject.FindGameObjectWithTag("DiaryContent");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            callTempMeshRend.materials = matArrayOutline;
            clickable.enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck"))
        {
            callTempMeshRend.materials = matArrayNormal;
            clickable.enabled = false;
        }
    }

    public void DisableTextCover()
    {
        Diary.Instance.currentPage = diaryPage;
        Diary.Instance.ButtonsVisibility(diaryPage, Diary.diaryList);
        Diary.Instance.HiddenContent(diaryPage, Diary.diaryList);
        Diary.Instance.OpenButton.SetActive(false);
        Diary.Instance.DiaryEntry.SetActive(true);
        diaryContainer.transform.GetChild(0).gameObject.SetActive(true);
        childContent.transform.GetChild(diaryPage).gameObject.SetActive(true);
        isTriggered = true;
        observer.Trigger();
    }

    public void Outline()
    {
        callTempMeshRend = GetComponent<MeshRenderer>();
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
            isTriggered = true;
        }
    }
}
