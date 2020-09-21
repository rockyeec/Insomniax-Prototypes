using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogState : MonoBehaviour
{
    public float moveForce = 0f;
    public Vector3 moveDir;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDir = ChooseDir();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveDir * moveForce;
    }

    Vector3 ChooseDir()
    {
        System.Random rand = new System.Random();
        int i = rand.Next(0, 1);
        Vector3 temp = new Vector3();

        if (i == 0)
        {
            temp = transform.forward;
        }
        else if (i == 1)
        {
            temp = transform.right;
        }
        return temp;
    }

}
