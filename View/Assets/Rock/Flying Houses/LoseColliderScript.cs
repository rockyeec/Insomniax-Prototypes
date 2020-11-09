using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColliderScript : MonoBehaviour
{
    //PlayerInput player;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer != 20)
            return;

        LevelManager.ResetLevel();
    }

        /*StartCoroutine(ResetToNormal());

        if (player == null)
            player = collision.collider.GetComponent<PlayerInput>();

        player.PressGlassesButton();
    }

    IEnumerator ResetToNormal()
    {
        SceneTransitionFader.FadeIn();
        yield return new WaitForSeconds(0.69f);
        SceneTransitionFader.FadeOut();
    }*/
}
