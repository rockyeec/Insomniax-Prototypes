using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScript : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PlayerInput.MoveSpeed = 0.5f; // does this not work?
            //Debug.Log(PlayerInput.MoveSpeed);
            Rigidbody r = other.GetComponent<InputParent>().Rb;
            if (r != null)
            {
                r.AddForce(-transform.forward * 500f, ForceMode.Acceleration);
            }

        }
    }
}
