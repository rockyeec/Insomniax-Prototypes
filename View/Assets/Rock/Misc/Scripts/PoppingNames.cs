using System.Collections;
using TMPro;
using UnityEngine;

public class PoppingNames : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp = null;
    private string text = string.Empty;
    private readonly float durationBetweenLetters = 0.05f;
    private bool isTyped = false;

    private void Start()
    {
        text = tmp.text;
        tmp.text = string.Empty;
    }

    public void TriggerTypeText()
    {
        if (isTyped)
            return;

        isTyped = true;
        StopAllCoroutines();
        StartCoroutine(TypeText());
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
