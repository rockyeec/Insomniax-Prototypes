using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{
    void Start()
    {
        SaveData data = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(data.level); 
    }
}
