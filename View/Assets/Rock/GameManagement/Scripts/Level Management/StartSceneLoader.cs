using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(LevelManager.CurrentLevel);
    }
}
