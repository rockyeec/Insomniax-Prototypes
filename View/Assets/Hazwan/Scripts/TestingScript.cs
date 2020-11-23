using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(testing());
    }


    IEnumerator testing()
    {
        yield return new WaitForSeconds(3);
        EntryPrompt.Instance.PromptActivation(6);
    }
}
