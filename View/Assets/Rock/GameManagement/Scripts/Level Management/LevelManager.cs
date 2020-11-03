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
            SaveData data = SaveSystem.LoadData();

            if (data == null || data.level == 0)
            {
                SaveSystem.SavePlayer(Vector3.zero, 1);
            }
            return SaveSystem.LoadData().level;
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
        SaveSystem.SavePlayer(Vector3.zero, nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
