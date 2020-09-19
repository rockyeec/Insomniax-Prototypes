using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
