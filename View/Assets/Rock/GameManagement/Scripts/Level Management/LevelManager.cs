using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    public static int CurrentLevel
    {
        get 
        {
            if (!PlayerPrefs.HasKey("currentLevel") || PlayerPrefs.GetInt("currentLevel") == 0)
                PlayerPrefs.SetInt("currentLevel", 1);
            return PlayerPrefs.GetInt("currentLevel");
            //SaveData data = SaveSystem.LoadPlayer();
            //return data.level;
        }
    }

    public static void LoadNextLevel()
    {
        instance.StartCoroutine(instance.Transition());
    }
    public static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Transition()
    {
        SceneTransitionFader.FadeIn();

        yield return new WaitForSeconds(0.75f);

        int nextLevel = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        PlayerPrefs.SetInt("currentLevel", nextLevel);
        //SaveSystem.LoadPlayer().level = nextLevel;
        SceneManager.LoadScene(nextLevel);
    }
}
