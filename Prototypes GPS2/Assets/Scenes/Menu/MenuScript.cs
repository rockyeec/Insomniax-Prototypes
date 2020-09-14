using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void GoToView()
    {
        StartCoroutine(LoadScene(1));
    }

    public void GoToLittleSaviour()
    {
        StartCoroutine(LoadScene(2));
    }

    public void GoToCovidHalloween()
    {
        StartCoroutine(LoadScene(3));
    }

    IEnumerator LoadScene(int i)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(i);
    }
}
