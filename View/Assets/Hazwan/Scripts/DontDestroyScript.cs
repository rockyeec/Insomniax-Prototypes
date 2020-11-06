using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {

    }

    void Update()
    {

    }
}
