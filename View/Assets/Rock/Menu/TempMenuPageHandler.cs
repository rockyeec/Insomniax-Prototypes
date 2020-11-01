using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempMenuPageHandler : MonoBehaviour
{
    [SerializeField] float padding = 10.0f;
    private UISlidingAnimation[] animatedChildren = null;

    [Header("Interactions")]
    [SerializeField] TempMenuPageHandler previousPage = null;
    [SerializeField] Button openButton = null;
    [SerializeField] Button closeButton = null;


    //private void Awake()
    //{
    //    animatedChildren = transform.GetComponentsInChildren<UISlidingAnimation>();
    //    openButton.onClick.AddListener(OnOpenPress);
    //    closeButton.onClick.AddListener(OnClosePress);
    //}
    void OnOpenPress()
    {
        if (Input.touchCount > 1)
            return;
        if (previousPage != null)
            previousPage.SlideOut();
        SlideIn();
    }
    void OnClosePress()
    {
        if (Input.touchCount > 1)
            return;
        if (previousPage != null)
            previousPage.SlideIn();
        SlideOut();
    }
    private void Start()
    {
        animatedChildren = transform.GetComponentsInChildren<UISlidingAnimation>();
        openButton.onClick.AddListener(OnOpenPress);
        closeButton.onClick.AddListener(OnClosePress);
        foreach (var item in animatedChildren)
        {
            item.OriginalPosition = item.transform.position;
        }
        SnapOut();
    }

    public void Init()
    {
        InitRectTransform();
        InitChildrenPositions();
        InitAnimations();        
    }

    void InitChildrenPositions()
    {
        float totalHeight = (transform.childCount - 1) * padding;
        for (int i = 0; i < transform.childCount; i++)
        {
            totalHeight += transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }
        float startY = totalHeight / 2.0f;
        float curY = startY;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);

            rectTransform.anchoredPosition = 
                new Vector2(i == 0 ? - 80.0f : 0.0f,
                curY - rectTransform.sizeDelta.y / 2.0f);
            curY -= (rectTransform.sizeDelta.y + padding);
        }
    }

    void InitRectTransform()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(1, 0.5f);
        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.sizeDelta = new Vector2(400.0f, 400.0f);
    }

    void InitAnimations()
    {
        float minOvershoot = 0.60f;
        float maxOvershoot = 0.80f;
        int length = transform.childCount;
        for (int i = 0; i < length; i++)
        {
            GameObject curChild = transform.GetChild(i).gameObject;
            UISlidingAnimation anim = curChild.GetComponent<UISlidingAnimation>();
            if (anim == null)
            {
                anim = curChild.AddComponent<UISlidingAnimation>();
            }

            anim.PercentageOvershoot
                = Mathf.Lerp(minOvershoot, maxOvershoot, 1.0f - (float)i / (float)(length - 1));
            anim.OutwardsDirection
                = new Vector3(0.420f, 0.420f);
        }
    }

    public void SlideIn()
    {
        FillAnimatedChildren();
        foreach (var item in animatedChildren)
        {
            item.SlideIn();
        }
    }
    public void SlideOut()
    {
        FillAnimatedChildren();
        foreach (var item in animatedChildren)
        {
            item.SlideOut();
        }
    }
    public void SnapIn()
    {
        FillAnimatedChildren();
        foreach (var item in animatedChildren)
        {
            item.SnapIn();
        }
    }
    public void SnapOut()
    {
        FillAnimatedChildren();
        foreach (var item in animatedChildren)
        {
            item.SnapOut();
        }
    }

    private void FillAnimatedChildren()
    {
        if (animatedChildren == null)
            animatedChildren = transform.GetComponentsInChildren<UISlidingAnimation>();
    }
}
