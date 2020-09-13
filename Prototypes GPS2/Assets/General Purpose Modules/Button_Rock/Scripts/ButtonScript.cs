using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class ButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public bool IsHeld { get; private set; }
    public bool IsDown { get; private set; }
    public bool IsUp { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHeld = true;
        StartCoroutine(ResetIsDown());
        canvasGroup.alpha = 0.4f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHeld = false;
        StartCoroutine(ResetIsUp());
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }

    /*void Update()
    {
        if (IsDown) Debug.Log("Down");
        if (IsUp) Debug.Log("Up");
    }*/

    IEnumerator ResetIsDown()
    {
        IsDown = true;
        yield return new WaitForEndOfFrame();
        IsDown = false;
    }

    IEnumerator ResetIsUp()
    {
        IsUp = true;
        yield return new WaitForEndOfFrame();
        IsUp = false;
    }
}
