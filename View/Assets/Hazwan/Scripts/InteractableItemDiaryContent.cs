using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableItemDiaryContent : MonoBehaviour
{
    [SerializeField]
    private GameObject content = null;

    MeshRenderer callTempMeshRend;

    Material[] matArrayOutline;
    Material[] matArrayNormal;

    bool isTriggered = false;

    void Start()
    {
        callTempMeshRend = GetComponent<MeshRenderer>();
        matArrayNormal = callTempMeshRend.materials;
        List<Material> listMaterial = new List<Material>();
        listMaterial.AddRange(matArrayNormal);
        listMaterial.Add(MaterialManager.OutLineMaterial);
        matArrayOutline = listMaterial.ToArray();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            Diary.diaryContent.Add(content);
            gameObject.SetActive(false);
            callTempMeshRend.materials = matArrayOutline;
            print(Diary.diaryContent.Count);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            isTriggered = true;
            callTempMeshRend.materials = matArrayNormal;
        }
    }
}
