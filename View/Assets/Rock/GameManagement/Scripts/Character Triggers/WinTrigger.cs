using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            PlayerInput player = other.GetComponent<PlayerInput>();
            if (player != null)
                LevelManager.LoadNextLevel();
        }
    }
}
