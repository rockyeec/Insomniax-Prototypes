using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    TextCoverScript textCoverScript;
    Camera cam = null;

    public List<GameObject> ObjectName = new List<GameObject>();

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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 << 10))
        {
            GameObject touchedObject = hit.transform.gameObject;
            Debug.Log("Touched " + touchedObject.transform.name);

            for (int i = 0; i < ObjectName.Count; i++)
            {
                if (hit.transform.gameObject.name == ObjectName[i].transform.name)
                {
                    textCoverScript.DisableTextCover();
                    enabled = false;
                }
            }

            
            
        }
    }
}



//hit.collider.GetComponent<InteractableItemDiaryContent>()