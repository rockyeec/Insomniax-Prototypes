using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBangTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            if (other.gameObject.name == "Player")
                AudioManager.instance.Play("CarAccident", "SFX");
        }
    }
}
