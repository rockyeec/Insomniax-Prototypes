using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHighLighter : MonoBehaviour
{
    [SerializeField] Image highlight = null;
    [SerializeField] TextMeshProUGUI text = null;

    public void Highlight(Highlightable highlightable)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(MoveTowards(highlightable));
    }
    public void TurnOff()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator MoveTowards(Highlightable highlightable)
    {
        text.text = string.Empty;
        Vector2 startSize = highlight.rectTransform.sizeDelta;
        Vector2 targetSize = highlightable.RectTransform.sizeDelta;
        Vector3 startPos = transform.position;
        Vector3 targetPos = highlightable.transform.position;
        Color startColor = highlight.color;
        Color targetColor = highlight.color.WithAlpha(1.0f);

        float elapsed = 0.0f;
        float duration = CurveManager.AnimationDuration;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = CurveManager.Curve.Evaluate(elapsed / duration);

            highlight.rectTransform.sizeDelta = Vector2.LerpUnclamped(startSize, targetSize, t);
            transform.position = Vector3.LerpUnclamped(startPos, targetPos, t);
            highlight.color = Color.LerpUnclamped(startColor, targetColor, t);

            yield return null;
        }

        highlight.rectTransform.sizeDelta = targetSize;
        transform.position = targetPos;
        highlight.color = targetColor;
        text.text = highlightable.Description;

        StartCoroutine(Beat());
    }

    IEnumerator Beat()
    {
        float elapsed = 0.0f;
        float beatLength = 0.3f;
        Color a = highlight.color.WithAlpha(0.0f);
        Color b = highlight.color.WithAlpha(1.0f);
        while (true)
        {
            yield return null;
            elapsed += Time.deltaTime;
            if (elapsed >= beatLength)
            {
                elapsed -= beatLength;
                Color swap = a;
                a = b;
                b = swap;
            }
            float t = CurveManager.FadeCurve.Evaluate(elapsed / beatLength);
            highlight.color = Color.Lerp(a, b, t);
        }
    }


    private void Awake()
    {
        GameScript.OnPause += GameScript_OnPause;
        GameScript.OnPlay += GameScript_OnPlay;
    }

    private void OnDestroy()
    {
        GameScript.OnPause -= GameScript_OnPause;
        GameScript.OnPlay -= GameScript_OnPlay;
    }

    private void GameScript_OnPlay()
    {
        highlight.enabled = true;
        text.enabled = true;
    }

    private void GameScript_OnPause()
    {
        highlight.enabled = false;
        text.enabled = false;
    }
}
