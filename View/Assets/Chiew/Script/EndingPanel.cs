using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
 
    //Variable as below
    //private

    //public

    // Start is called before the first frame update
    void Start()
    {
        PanelOnOfF(true);
        //if (debug == true) { PanelOnOfF(true); }
        //else { PanelOnOfF(false); }
    }

    // Update is called once per frame
    void Update()
    {
        
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
