using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPropLeftRight : MonoBehaviour
{
    [SerializeField] Transform[] things = null;
    List<Vector3> axes = new List<Vector3>();
    List<float> speeds = new List<float>();

    void Start()
    {
        things = GetComponentsInChildren<Transform>();
        List<Transform> list = new List<Transform>();
        list.AddRange(things);
        list.Remove(transform);
        things = list.ToArray();
        for (int i = 0; i < things.Length; i++)
        {
            axes.Add(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
            speeds.Add(Random.Range(13.37f, 26.9f));
        }
    }

    void Update()
    {
        for (int i = 0; i < things.Length; i++)
        {
            things[i].Rotate(axes[i], speeds[i] * Time.deltaTime);
        }
    }
}
