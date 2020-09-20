using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    readonly private float fanSpeed = 420.0f;
    private void Update()
    {
        transform.Rotate(transform.up, Time.deltaTime * fanSpeed);
    }
}
