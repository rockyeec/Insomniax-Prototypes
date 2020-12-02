using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0StartTriggers : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayBgm("Main Music Normal");
    }
}
