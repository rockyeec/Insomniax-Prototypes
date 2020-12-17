using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBackgroundStuff : MonoBehaviour
{
    float speed = 1.0f;
    [SerializeField] Transform[] stuffs = null;

    Vector3[] axes = null;

    private void Start()
    {
        speed = Random.Range(13.37f, 69.0f);
        axes = new Vector3[stuffs.Length];
        for (int i = 0; i < stuffs.Length; i++)
        {
            axes[i] = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);

        for (int i = 0; i < stuffs.Length; i++)
        {
            stuffs[i].Rotate(axes[i], 2.0f * speed * Time.deltaTime);
        }
    }
}
