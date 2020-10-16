using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingNames : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    private string text = string.Empty;
    private readonly float durationBetweenLetters = 0.05f;
    private bool isTyped = false;

    private void Start()
    {
        text = tmp.text;
        tmp.text = string.Empty;

        spriteRenderer.enabled = false;
    }

    public void TriggerTypeText()
    {
        if (isTyped)
            return;

        isTyped = true;
        StopAllCoroutines();
        StartCoroutine(TypeText());
        spriteRenderer.enabled = true;
    }

    private IEnumerator TypeText()
    {
        float time = Time.time + durationBetweenLetters;
        foreach (char item in text)
        {
            tmp.text += item;

            while (Time.time < time)
            {
                yield return null;
            }
            time = Time.time + durationBetweenLetters;
        }
    }
}
