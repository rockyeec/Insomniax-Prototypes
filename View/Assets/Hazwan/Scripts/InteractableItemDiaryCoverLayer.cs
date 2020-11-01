using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableItemDiaryCoverLayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textCover;

    bool isTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("TriggerCheck") && !isTriggered)
        {
            isTriggered = true;
            gameObject.SetActive(false);
            TextCoverScript.Instance.DisableTextCover(textCover);
        }
    }
}
