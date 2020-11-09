using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPictureScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().sortingOrder = 100;
    }
}
