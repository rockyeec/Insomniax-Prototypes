using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private GameObject wall = null;

    void Start()
    {
        GameScript.OnGlassesOn += DisableWallObject;
        GameScript.OnGlassesOff += EnableWallObject; 
    }

    void EnableWallObject()
    {
        wall.SetActive(true);
    }

    void DisableWallObject()
    {
        wall.SetActive(false);
    }
}
