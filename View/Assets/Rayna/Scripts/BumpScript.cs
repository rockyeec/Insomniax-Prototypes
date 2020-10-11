using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScript : MonoBehaviour
{

    public GameObject player;
    public Rigidbody r;
    void Start()
    {
        r = GetComponent<InputParent>().Rb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //PlayerInput.MoveSpeed = 0.5f;
            Debug.Log(PlayerInput.MoveSpeed);
            r.AddForce(-transform.forward * 5f, ForceMode.Acceleration);

        }
    }
}
