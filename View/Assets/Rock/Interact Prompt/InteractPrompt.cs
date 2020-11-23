using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField]
    Image picture = null;
    [SerializeField]
    Vector3 offset = Vector3.up;

    Camera cam;
    Transform target;

    static InteractPrompt instance;
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public static void DoThing(Transform t)
    {
        instance.gameObject.SetActive(true);
        instance.target = t;       
    }

    public static void UndoThing()
    {
        instance.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        picture.transform.position = cam.WorldToScreenPoint(target.position) + offset;
    }
}
