using System.Collections;
using UnityEngine;
using TMPro;

public class InteractableSubject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] coverArr = null;

    private void Awake()
    {
        InteractableObserver.OnInteracted += InteractableObserver_OnInteracted;

        for (int i = 0; i < coverArr.Length; i++)
        {
            string entryName = "Entry " + i.ToString();
            if (SaveSystem.GetBool(entryName))
            {
                coverArr[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnDestroy()
    {
        InteractableObserver.OnInteracted -= InteractableObserver_OnInteracted;
    }

    private void InteractableObserver_OnInteracted(int i)
    {
        SaveSystem.SetBool("Entry " + i.ToString(), true);

        float duration = 2.0f;
        coverArr[i].CrossFadeAlpha(0.0f, duration, true);
        StartCoroutine(DelaySetActiveFalse(i, duration));
    }

    IEnumerator DelaySetActiveFalse(int i, float duration)
    {
        yield return new WaitForSeconds(duration);
        coverArr[i].gameObject.SetActive(false);
    }
}
