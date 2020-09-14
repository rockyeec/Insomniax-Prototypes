using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        StartCoroutine(LoadScene(0));
    }
    IEnumerator LoadScene(int i)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(i);
    }
}
