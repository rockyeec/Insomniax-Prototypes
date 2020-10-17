using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        if (rb == null)
            rb = GetComponentInParent<Rigidbody>();

        if (rb == null)
            return;

        if (other.gameObject.layer == 11)
        {
            other.GetComponent<Rigidbody>().AddForce(rb.velocity * 1.1f, ForceMode.Impulse);
        }
    }
}
