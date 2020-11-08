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
            if (SaveSystem.GetInt("level") == 0)
            {
                SaveSystem.Reset();
                SaveSystem.SetInt("level", 1);
            }
            return SaveSystem.GetInt("level");
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
        SaveSystem.SetInt("level", nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
