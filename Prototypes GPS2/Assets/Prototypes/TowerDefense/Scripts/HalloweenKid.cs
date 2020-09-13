using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class KidsRoute
{
    public Transform[] points;
}


public class HalloweenKid : MonoBehaviour
{
    Queue<Vector3> route = new Queue<Vector3>();
    Vector3 curPoint;

    public void Init(KidsRoute route)
    {
        foreach (var item in route.points)
        {
            this.route.Enqueue(item.position);
        }
        curPoint = this.route.Dequeue();

        Transform caps = GetComponentInChildren<MeshRenderer>().transform;
        caps.position += Vector3.up * UnityEngine.Random.Range(0.0f, 4.20f) + Vector3.right * UnityEngine.Random.Range(-1.337f, 1.337f);
    }

    void Update()
    {
        if (Vector3.Distance(curPoint, transform.position) <= 2.5f)
        {
            if (route.Count != 0)
                curPoint = route.Dequeue();
            else
                Destroy(gameObject);
        }
        else
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, curPoint, 4.20f * Time.deltaTime);
            transform.position = pos;
        }
    }
}
