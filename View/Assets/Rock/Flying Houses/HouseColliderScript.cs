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
            InvokerForMonologue.Do("ShakeScreen");
            player.GetComponentInChildren<Animator>().CrossFade("Hurt", 0.15f);
            if (PlayerInput.MoveSpeed > 0.0f)
                StartCoroutine(MakeSlow());
        }
    }

    IEnumerator MakeSlow()
    {
        if(PlayerInput.MoveSpeed >= 0.420f)
            PlayerInput.MoveSpeed -= 0.420f;
        yield return new WaitForSeconds(0.6f);
        if (PlayerInput.MoveSpeed <= 0.84f) 
            PlayerInput.MoveSpeed += 0.420f;
    }
}
