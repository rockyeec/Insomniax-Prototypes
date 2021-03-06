﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TempMenuHandler : MonoBehaviour
{
    [SerializeField] private Button menuButton = null;
    [SerializeField] private Button playButton = null;
    [SerializeField] private TempMenuPageHandler pauseMenu = null;

    static bool isFirstLevel = true;

    private void Start()
    {
        menuButton.onClick.AddListener(GameScript.Pause);
        playButton.onClick.AddListener(GameScript.Unpause);

        StartCoroutine(SnapToPosition());
    }

    IEnumerator SnapToPosition()
    {
        yield return new WaitForEndOfFrame();
        if (isFirstLevel)
        {
            isFirstLevel = false;
            GameScript.Pause();
            pauseMenu.SnapIn();
        }
    }

    public void OnExitPress()
    {
        if (Input.touchCount > 1)
            return;
        Application.Quit();
    }
}
