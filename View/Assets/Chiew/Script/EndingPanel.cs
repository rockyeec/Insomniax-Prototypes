using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingPanel : MonoBehaviour
{
    [Header("Tick if wanted debug")]
    public bool debug = false;

    [Header("Element TO Check needed to display")]
    public bool withPicture = false;

    [Header("Needed manually assign")]
    public GameObject buttonsParents;
    public Image imagesBackground;
    public GameObject holder;
    public TextMeshProUGUI TMP_UI;

    [Header("What text you wanted to show on it")]
    public string Text_Here;
 
    void Start()
    {
        PanelOnOfF(false);
        //if (debug == true) { PanelOnOfF(true); }
        //else { PanelOnOfF(false); }
    }

    public void PanelOnOfF(bool o)
    {
        if(o == true)
        {
            holder.SetActive(true);
            if(withPicture == true)
            { imagesBackground.gameObject.SetActive(true); }
            else { imagesBackground.gameObject.SetActive(false); }
            buttonsParents.SetActive(true);
            TMP_UI.gameObject.SetActive(true);
            TMP_UI.text = Text_Here;
            Destroy(FindObjectOfType<DontDestroyScript>().gameObject);
        }
        else
        {
            holder.SetActive(false);
            buttonsParents.SetActive(false);
        }
    }

    //for buttons usage
    public void closeGame()
    {
        Application.Quit();
        DebugFunction("Quit");
    }

    public void returnToMainMenu()
    {
        //change to scene 1
        //LevelManager.changescene(1);
        DebugFunction("Change to main menu");
        LevelManager.LoadNextLevel();
    }


    private void DebugFunction(string d)
    {
        if(debug == true) { Debug.Log(d); }
    }
}
