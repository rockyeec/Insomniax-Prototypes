using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    public GameObject cube;

    public bool isFake = false;

    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();    
    }

    void Update()
    {
        DestroyGameObject();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "GroundCheck" && isFake)
        {
            Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
            cubeRigidbody.isKinematic = false;
        }
    }

    void DestroyGameObject()
    {
        if (transform.position.y < -10)
        {
            col.enabled = !col.enabled;
            Destroy(gameObject);
        }
    }
}
