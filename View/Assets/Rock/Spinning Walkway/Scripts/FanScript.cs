using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    readonly private float fanSpeed = 420.0f;
    private void FixedUpdate()
    {
        transform.Rotate(transform.up, Time.fixedDeltaTime * fanSpeed);
    }
}
