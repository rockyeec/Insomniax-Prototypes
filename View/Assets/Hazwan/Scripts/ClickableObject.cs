using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    TextCoverScript textCoverScript;
    Camera cam = null;

    public static int totalofInteracted = 0;

    public void Init(TextCoverScript tcs)
    {
        textCoverScript = tcs;
        cam = Camera.main;
        enabled = false;
    }
    void Update()
    {

        if (!Input.GetMouseButtonDown(0))
            return;

        if (cam == null)
            cam = Camera.main;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, 1 << 10))
        {
            if (hit.collider.gameObject == gameObject)
            {
                InteractPrompt.UndoThing();
                textCoverScript.DisableTextCover();
                enabled = false;
                totalofInteracted++;
                print(totalofInteracted);
            }
        }

        if(totalofInteracted == 5)
        {
            EntryPrompt.Instance.PromptActivation(9);
        }
    }
}



//hit.collider.GetComponent<InteractableItemDiaryContent>()