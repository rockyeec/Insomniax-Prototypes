using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    [Header("Only for my scene Testing purpose")]
    public bool debug = false;

    [Header("All Children")]
    public GameObject Holder;
    public GameObject buttonsObj;
    public Image imagesBackground;

    [Header("Ending Panel (For After done reading)")]
    public EndingPanel edPanel;

    // Start is called before the first frame update
    void Start()
    {
        PanelOnOfF(false);
    }

    public void buttonFunction()
    {
        if(edPanel != null)
        {
            this.PanelOnOfF(false);
            edPanel.PanelOnOfF(true); //on endingpanel
        }
        DebugFunction("Done Reading and next");
    }

    public void PanelOnOfF(bool o)
    {
        if (o == true)
        {
            Holder.SetActive(true);
            imagesBackground.gameObject.SetActive(true);
            buttonsObj.SetActive(true);
            DebugFunction("Panel On");
        }
        else
        {
            Holder.SetActive(false);
            buttonsObj.SetActive(false);
            imagesBackground.gameObject.SetActive(false);
            DebugFunction("Panel Off");
        }
    }

    private void DebugFunction(string d)
    {
        if (debug == true) { Debug.Log(d); }
    }

}
