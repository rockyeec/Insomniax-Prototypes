using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSFXTesting : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.instance.PlaySfx("Shoot");
        }
    }
}
