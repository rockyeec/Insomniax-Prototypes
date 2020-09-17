using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform stick = null;
    [SerializeField] private RectTransform joystickZone = null;
    [SerializeField] private bool isLimitStick = true;

    private Canvas canvas = null;
    private CanvasGroup stickCanvasGroup = null;

    private float bgSqrRadius;
    private float bgRadius;

    private Vector2 oldDragDelta;
    private Vector2 dragDelta;

    private void Start()
    {
        canvas = ContinuouslyGetComponentInParents<Canvas>();
        stickCanvasGroup = stick.GetComponent<CanvasGroup>();

        stick.anchoredPosition = Vector2.zero;
        bgRadius = joystickZone.rect.width / 2;
        bgSqrRadius = Mathf.Pow(bgRadius, 2);
    }

    private void LateUpdate()
    {
        if (oldDragDelta == dragDelta)
        {
            dragDelta.x = dragDelta.y = 0.0f;
        }
        oldDragDelta = dragDelta;
    }

    private T ContinuouslyGetComponentInParents<T>() where T : Component
    {
        Transform cur = transform.parent;
        while (cur != null)
        {
            T component = cur.GetComponent<T>();
            if (component != null)
                return component;
            cur = cur.parent;
        }
        return null;
    }

    public float GetHorizontal()
    {
        return stick.anchoredPosition.x / bgRadius;
    }
    public float GetVertical()
    {
        return stick.anchoredPosition.y / bgRadius;
    }
    public float GetHorizontalDelta()
    {
        return dragDelta.x;
    }
    public float GetVerticalDelta()
    {
        return dragDelta.y;
    }    

    public void OnPointerDown(PointerEventData eventData)
    {
        stickCanvasGroup.alpha = 0.4f;
        stickCanvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        dragDelta = eventData.delta / canvas.scaleFactor;

        stick.anchoredPosition += dragDelta;

        // avoid unecessary sqrt operation
        if (!isLimitStick)
            return;

        if (stick.anchoredPosition.sqrMagnitude > bgSqrRadius)
        {
            stick.anchoredPosition = stick.anchoredPosition.normalized * bgRadius;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        stickCanvasGroup.alpha = 1.0f;
        stickCanvasGroup.blocksRaycasts = true;
        stick.anchoredPosition 
            = dragDelta
            = Vector2.zero;
    }
}
