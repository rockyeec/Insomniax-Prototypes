using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexRoomScript : MonoBehaviour
{
    private void Start()
    {
        SpinningSegmentArranger.OnStartSpinning += SpinningSegmentArranger_OnStartSpinning;
    }

    private void SpinningSegmentArranger_OnStartSpinning()
    {
        StopAllCoroutines();
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        float elasped = 0.0f;
        float duration = 1.337f;

        Vector3 a = transform.position;
        Vector3 b = Vector3.down * 69.0f;

        while (elasped < duration)
        {
            elasped += Time.deltaTime;

            float t = elasped / duration;

            transform.position = Vector3.Lerp(a, b, t);

            yield return null;
        }
        transform.position = b;
    }
}
