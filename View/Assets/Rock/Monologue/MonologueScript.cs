using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonologueScript : MonoBehaviour
{
    [SerializeField] Button skipButton = null;
    [SerializeField] TextMeshPro text = null;
    [SerializeField] SpriteRenderer ren = null;
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

        gameObject.SetActive(false);
    }

    void Skip()
    {
        isSkip = true;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
            isSkip = true;
#endif

        if (Time.timeScale == 0.0f)
            return;

        if (cam == null)
            cam = Camera.main;

        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
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
        StartCoroutine(Fade(true));

        while (monologue.Count != 0)
        {
            text.text = string.Empty;
            string stringCur = monologue.Dequeue();
            if (InvokerForMonologue.ContainsCommand(stringCur))
            {
                InvokerForMonologue.Do(stringCur);
            }
            else
            {
                // type out sentence
                foreach (var item in stringCur)
                {
                    if (isSkip)
                        break;

                    yield return new WaitForSeconds(0.015f);
                    text.text += item;
                }

                // short pause after every sentence
                float duration = 1.2f;
                float time = Time.time + duration;
                while (Time.time < time)
                {
                    if (isSkip)
                    {
                        isSkip = false;
                        break;
                    }
                    yield return null;
                }
            }            
        }

        StartCoroutine(Fade(false));
    }

    

    IEnumerator Fade(bool isActive)
    {
        float elapsed = 0.0f;
        float duration = 0.3f;
        Color ori = ren.color;
        Color target = ren.color.WithAlpha(isActive ? 1.0f : 0.0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = CurveManager.Curve.Evaluate(elapsed / duration);

            ren.color = Color.LerpUnclamped(ori, target, t);

            if (skipButton != null)
                skipButton.image.color = Color.LerpUnclamped(ori, target, t);

            yield return null;
        }

        ren.color = target;
        gameObject.SetActive(isActive);
        if (skipButton != null)
            skipButton.gameObject.SetActive(isActive);
    }
}
