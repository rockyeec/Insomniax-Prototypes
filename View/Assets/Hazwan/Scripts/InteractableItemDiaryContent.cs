using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemDiaryContent : MonoBehaviour
{
    [SerializeField]
    private GameObject content = null;

    [SerializeField]
    private GameObject prompt = null;

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
            Diary.diaryList.Add(content);
            callTempMeshRend.materials = matArrayOutline;
            //print(Diary.diaryContent.Count);
            StartCoroutine(Prompt());
            
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

    IEnumerator Prompt()
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(2f);
        prompt.SetActive(false);
        gameObject.SetActive(false);
    }
}
