﻿using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Material wallMaterial = null;
    [SerializeField] private Material fadedOutMaterial = null;
    [SerializeField] private Material outLineMaterial = null;

    private static MaterialManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public static Material FadedOutMaterial { get { return instance.fadedOutMaterial; } }
    public static Material WallMaterial { get { return instance.wallMaterial; } }
    public static Material OutLineMaterial { get { return instance.outLineMaterial; } }
}
