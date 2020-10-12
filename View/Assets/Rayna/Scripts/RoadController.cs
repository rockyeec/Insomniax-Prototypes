using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] roadSegments;

    public Transform resetPoint;

    public float speed;

    void Update()
    {

        MoveRelativeToOther();
    }

    public void MoveRelativeToOther()
    {

        Transform firstSegment = roadSegments[0];
        Transform secondSegment = roadSegments[1];

        foreach (Transform i in roadSegments)
        {

            i.position += -transform.forward * speed * Time.deltaTime;
        }

        if (firstSegment.position.z <= resetPoint.position.z)
        {

            firstSegment.position = new Vector3(firstSegment.position.x, firstSegment.position.y, secondSegment.position.z + secondSegment.GetChild(0).localScale.z);
        }

        if (secondSegment.position.z <= resetPoint.position.z)
        {

            secondSegment.position = new Vector3(secondSegment.position.x, secondSegment.position.y, firstSegment.position.z + firstSegment.GetChild(0).localScale.z);
        }
    }

    public void MoveRelativeToSelf()
    {

        foreach (Transform i in roadSegments)
        {

            i.position += -transform.forward * speed * Time.deltaTime;

            if (i.position.z <= resetPoint.position.z)
            {

                i.position = new Vector3(i.position.x, i.position.y, i.position.z + i.GetChild(0).localScale.z * 2);
            }
        }
    }
}
