using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public Transform player;
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision treadmill)
    {
        if (treadmill.gameObject.CompareTag("Treadmill"))
        {
            r.AddForce(-transform.forward * 5f, ForceMode.Acceleration);
        }
    }
}
