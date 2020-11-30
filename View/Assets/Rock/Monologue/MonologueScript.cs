using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonologueScript : MonoBehaviour
{
    [SerializeField] Button skipButton = null;
    [SerializeField] TextMeshProUGUI skipText = null;
    [SerializeField] TextMeshPro text = null;
    [SerializeField] Renderer ren = null;
    Camera cam;
    bool isSkip = false;

    private static MonologueScript instance;
    private void Awake()
    {
        instance = this;

        if (skipButton != null)
        {
            skipButton.onClick.AddListener(Skip);
            skipButton.gameObject.SetActive(false);
        }

        ren.sortingOrder = 2000;
        ren.material.color = ren.material.color.WithAlpha(0.0f);
        text.color = text.color.WithAlpha(0.0f);
        skipText.color = skipText.color.WithAlpha(0.0f);
        gameObject.SetActive(false);
    }

    void Skip()
    {
        isSkip = true;
    }

    private void Update()
    {
        if (Time.timeScale == 0.0f)
            return;

        if (cam == null)
            cam = Camera.main;

        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    public static void TriggerText(string[] monologues)
    {
        Queue<string> q = new Queue<string>();
        foreach (var item in monologues)
        {
            q.Enqueue(item);
        }
        TriggerText(q);
    }

    public static void TriggerText(string monologue)
    {
        Queue<string> q = new Queue<string>();
        q.Enqueue(monologue);
        TriggerText(q);
    }
    public static void TriggerText(Queue<string> monologue)
    {
        instance.gameObject.SetActive(true); 
        if (instance.skipButton != null)
            instance.skipButton.gameObject.SetActive(true);

        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.PrintOut(monologue));
    }

    IEnumerator PrintOut(Queue<string> monologue)
    {
        while (monologue.Count != 0)
        {
            text.text = string.Empty;
            string stringCur = monologue.Dequeue();
            if (InvokerForMonologue.ContainsCommand(stringCur))
            {
                StartCoroutine(Fade(0.0f, true));
                InvokerForMonologue.Do(stringCur);

                yield return null;
                while (InvokerForMonologue.IsHold)
                {
                    yield return null;
                }
            }
            else if (stringCur.Contains("WaitFor"))
            {
                if (stringCur.Length < 9)
                    continue;
                string numSubstring = stringCur.Substring(8);
                float seconds = float.Parse(numSubstring);

                StartCoroutine(Fade(0.0f, true));
                yield return new WaitForSeconds(seconds);
                StartCoroutine(Fade(1.0f, true));
            }
            else
            {
                StartCoroutine(Fade(1.0f, true));

                // type out sentence
                foreach (var item in stringCur)
                {
                    if (isSkip)
                    {
                        //text.text = stringCur;
                        //isSkip = false;
                        break;
                    }
                    yield return null;
                    text.text += item;
                }

                float duration = 2.5f;
                float time = Time.time + duration;
                while (!isSkip)
                {
                    if (Time.time > time)
                        break;

                    yield return null;
                }

                isSkip = false;
            }            
        }

        StartCoroutine(Fade(0.0f, false));
    }

    

    IEnumerator Fade(float alpha, bool isActive)
    {
        float elapsed = 0.0f;
        float duration = 0.3f;
        Color ori = ren.material.color;
        Color target = ren.material.color.WithAlpha(alpha);
        Color textColOri = text.color;
        Color textColTarget = text.color.WithAlpha(alpha);
        Color skipTextOri = skipText.color;
        Color skipTextTar = skipText.color.WithAlpha(alpha);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = CurveManager.Curve.Evaluate(elapsed / duration);

            ren.material.color = Color.LerpUnclamped(ori, target, t);
            text.color = Color.LerpUnclamped(textColOri, textColTarget, t);
            skipText.color = Color.LerpUnclamped(skipTextOri, skipTextTar, t);

            if (skipButton != null)
                skipButton.image.color = Color.LerpUnclamped(ori, target, t);

            yield return null;
        }

        ren.material.color = target;
        text.color = textColTarget;
        skipText.color = skipTextTar;
        gameObject.SetActive(isActive);
        if (skipButton != null)
            skipButton.gameObject.SetActive(isActive);
    }
}
