using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public GameObject cube;

    public bool isFake = false;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck" && isFake)
        {
            Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
            cubeRigidbody.isKinematic = false;
            cubeRigidbody.AddForce(0, -25, 0, ForceMode.Impulse);
        }
    }
}
