using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    Vector3 startPos, endPos, direction;
    float timeStart, timeFin, timeInterval;

    [SerializeField] float forceXY = 1f;
    [SerializeField] float forceZ = 50f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                timeStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                timeFin = Time.time;
                timeInterval = timeFin = timeStart;

                endPos = Input.GetTouch(0).position;

                direction = startPos - endPos;
                rb.AddForce(-direction.x * forceXY, -direction.y * forceXY, forceZ / timeInterval);

                //isTouch = true;
                //Destroy(gameObject, 3f);
            }
        }
    }
}
