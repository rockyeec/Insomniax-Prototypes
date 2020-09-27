using System.Collections;
using UnityEngine;

public class SpinningSegment : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 6.9f;

    private void Start()
    {
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
        enabled = false;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOn()
    {
        /*StopAllCoroutines();
        StartCoroutine(Spin());*/
        enabled = true;
    }
    private void GameScript_OnGlassesOff()
    {
        //StopAllCoroutines();
        enabled = false;
    }


    /*private IEnumerator Spin()
    {
        while (true)
        {
            transform.Rotate(transform.forward, Time.deltaTime * spinSpeed);
            yield return null;
        }
    }*/

    private void FixedUpdate()
    {
        transform.Rotate(transform.forward, Time.fixedDeltaTime * spinSpeed);
    }
}
