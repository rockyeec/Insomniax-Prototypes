using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSystem : MonoBehaviour
{
    bool shouldLerp = false;

    private float timeStartLerping;
    public float lerpTime;

    public Vector3 startPosition;
    public Vector3 endPosition;

    void Start()
    {
        StartLerping();
        startPosition = GetComponent<Transform>().transform.position;
        endPosition = GetComponent<Transform>().transform.position;
        endPosition.y = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLerp)
        {
            transform.position = Lerp(startPosition, endPosition, timeStartLerping, lerpTime);
        }
    }


    void StartLerping()
    {
        timeStartLerping = Time.time;

        shouldLerp = true;
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        var result = Vector3.Lerp(start, end, percentageComplete);

        return result;
    }
}
