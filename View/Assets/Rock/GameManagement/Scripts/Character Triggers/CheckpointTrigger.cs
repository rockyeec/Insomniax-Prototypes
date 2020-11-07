using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            if (other.name == "Player")
            {
                other.GetComponent<BackToOriginal>().SetTargetPosAndRot(transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
