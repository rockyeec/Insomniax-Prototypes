using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombStoneScript : MonoBehaviour
{
    bool isTriggered = false;

    public static bool doneDialoguePuzzle = false;

    MeshRenderer callTempMeshRend;

    Material[] matArrayOutline;
    Material[] matArrayNormal;

    public Letter letterObj;

    private bool doneOn = false;

    void Start()
    {
        Outline();
        callTempMeshRend.materials = matArrayOutline;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered && doneDialoguePuzzle)
        {
            isTriggered = true;
            StartCoroutine(Delay());
        }
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

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        InvokerForMonologue.Do("DisableCameraControl");
        InvokerForMonologue.Do("DisableJump");
        InvokerForMonologue.Do("DisableDiary");
        InvokerForMonologue.Do("DisableMoveControl");
        yield return new WaitForSeconds(2f);
        Letter();
    }

    void Letter()
    {
        if (letterObj != null && !doneOn)
        {
            letterObj.PanelOnOfF(true);
            doneOn = true;
        }
    }


}
