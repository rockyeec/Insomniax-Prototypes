using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{
    void Start()
    {
        //SaveData data = SaveSystem.LoadPlayer();
        //Debug.Log("Start Scene Loader" + data.level);
        //SceneManager.LoadScene(data.level);
        SceneManager.LoadScene(LevelManager.CurrentLevel);
    }
}
