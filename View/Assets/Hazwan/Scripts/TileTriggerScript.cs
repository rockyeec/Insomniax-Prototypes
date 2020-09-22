using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTriggerScript : MonoBehaviour
{
    BoxCollider box;
    private void Start()
    {
        box = GetComponent<BoxCollider>();
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        box.enabled = false;
        StopAllCoroutines();
        StartCoroutine(WaitSomeTime());
    }
    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(0.69f);
        box.enabled = true;
    }
}
