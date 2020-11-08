using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDiary : MonoBehaviour
{
    public Vector3 moveDiary;
    public GameObject diary;
    void Start()
    {
        moveDiary = new Vector3(0, 0, 0);
        diary.transform.position = moveDiary;
    }

    void Update()
    {
        
    }
}
