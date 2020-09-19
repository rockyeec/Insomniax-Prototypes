using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public enum PagesList
{
    MAIN_MENU = 0,
    OPTION_PAGE,
    INSTRUCTION_PAGE
}

public class MenuManager : MonoBehaviour
{
    public MainMenuPages[] Pages; //Use array because optimize with function array provided
    public GameObject BackButton;
    public GameObject MenuParents;

    void Start()
    {
        BacktoMainMenu();
    }

    public void BacktoMainMenu()
    {
        MenuParents.SetActive(true);
        foreach (MainMenuPages page in Pages)
        {
            if (page.type == 0)
            {
                page.self.SetActive(true);
                BackButton.SetActive(false);
                AudioManager.instance.Play("MainMenuBGM", "BGM");
            }
            else
            {
                page.self.SetActive(false);
                BackButton.SetActive(false);
            }
        }
    }
    
    public void OpenPage(int pageIndex)
    {
        MainMenuPages m = Array.Find(Pages, page => page.type == (PagesList)pageIndex); //Find Each Page you wanted
        if(m == null)
        {
            Debug.LogWarning("Page: " + (PagesList)pageIndex + " not found!");
            return;
        }
        if(pageIndex != 0)
        {
            foreach (MainMenuPages page in Pages)
            {
                if (page.type == 0)
                {
                    page.self.SetActive(false);
                    BackButton.SetActive(true);
                }
                if ((int)page.type == 1)
                {
                    AudioManager.instance.Play("OptionBGM", "BGM");
                }
            }
        }
        m.self.SetActive(true);
    }

    public void CloseMainMenu()
    {
        MenuParents.SetActive(false);
    }

    public void CloseApplication()
    {
        Debug.Log("QuitGame");//Debug Purpose;
        Application.Quit();
    }

}

[System.Serializable]
public class MainMenuPages
{
    public string pageName;
    public PagesList type;
    public GameObject self;
}