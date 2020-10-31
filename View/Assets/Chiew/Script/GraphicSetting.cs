using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DefaultQuality();  
    }

    public void DefaultQuality()
    {
        GraphicSettingButtons(1);
    }

    public void GraphicSettingButtons(int num)
    {
        if(num == 1)
        {
            //low
            QualitySettings.SetQualityLevel(0);
            Debug.Log("Change to Low");
        }
        else if(num == 2)
        {
            //med
            QualitySettings.SetQualityLevel(1);
            Debug.Log("Change to Med");
        }
        else
        {
            //high
            QualitySettings.SetQualityLevel(2);
            Debug.Log("Change to High");
        }
    }
}
