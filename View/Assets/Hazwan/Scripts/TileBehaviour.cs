using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileBehaviour : MonoBehaviour
{
    #region Singleton

    private static TileBehaviour _instance;

    public static TileBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameObject");
                go.AddComponent<TileBehaviour>();
            }
            return _instance;
        }
    }
    #endregion



    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //GameScript.OnGlassesOn += StartTileProgram;
    }

    private void Update()
    {
        
    }
}
