using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreation : MonoBehaviour
{
    [SerializeField]
    GameObject[] tileList = null;
    
    void Start()
    {
        AudioManager.instance.PlayBgm("Main Music Glasses");
        TileMain.TileList.Clear();
        TileMain.TileList.AddRange(tileList);
    }
}
