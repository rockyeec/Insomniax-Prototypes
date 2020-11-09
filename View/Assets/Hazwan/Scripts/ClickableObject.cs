using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    TextCoverScript textCoverScript;
    Camera cam = null;

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
        if (Physics.Raycast(ray, 100.0f, 1 << 10))
        {
            textCoverScript.DisableTextCover();
            enabled = false;

        }
    }
}



//hit.collider.GetComponent<InteractableItemDiaryContent>()