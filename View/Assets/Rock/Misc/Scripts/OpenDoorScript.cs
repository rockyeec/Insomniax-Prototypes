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
            Rigidbody otherRb = other.GetComponent<Rigidbody>();
            if (otherRb != null)
                otherRb.AddForce(rb.velocity * 1.1f, ForceMode.Impulse);
        }
    }
}
