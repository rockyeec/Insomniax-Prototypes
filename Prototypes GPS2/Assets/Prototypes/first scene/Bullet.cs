using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(WaitForFixedUpdate());
    }

    IEnumerator WaitForFixedUpdate()
    {
        yield return new WaitForFixedUpdate();
        rb.velocity = (transform.forward * 1690.0f * Time.deltaTime);

        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.useGravity = true;
    }
}
