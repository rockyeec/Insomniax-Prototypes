using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheMainMenu : MonoBehaviour
{
    [SerializeField] Button start = null;
    [SerializeField] Button exit = null;
    [SerializeField] Button[] buttons = null;

    private void Awake()
    {
        start.onClick.AddListener(MyStart);
        exit.onClick.AddListener(Exit);
        foreach (var item in buttons)
        {
            item.onClick.AddListener(PlaySound);
        }
    }


    void MyStart() => SceneManager.LoadScene(LevelManager.CurrentLevel);
    void Exit() => Application.Quit();

    void PlaySound()
    {
        AudioManager.instance.PlaySfx("FlipBook");
    }
}
