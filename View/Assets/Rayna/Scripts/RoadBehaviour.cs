using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBehaviour : MonoBehaviour
{
    public RoadController controller;

    public float speed;

    void Update()
    {

        speed = controller.speed;

        transform.position += -transform.forward * speed * Time.deltaTime;

        if (transform.position.z <= controller.resetPoint.position.z)
        {

            transform.position = controller.resetPoint.position;
        }
    }

}
