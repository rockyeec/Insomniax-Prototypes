using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseColliderScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = 25;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 20)
            return;

        PlayerInput player = other.GetComponent<PlayerInput>();
        if (player != null)
        {
            Debug.Log("bigno");
            if (PlayerInput.MoveSpeed > 0.0f)
                StartCoroutine(MakeSlow());
        }
    }

    IEnumerator MakeSlow()
    {
        PlayerInput.MoveSpeed -= 0.25f;
        yield return new WaitForSeconds(0.6f);
        PlayerInput.MoveSpeed += 0.25f;
    }
}
