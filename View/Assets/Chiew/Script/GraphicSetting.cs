using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GraphicSettingButtons(int num)
    {
        if(num == 1)
        {
            //low
            Debug.Log("Change to Low");
        }
        else if(num == 2)
        {
            //med
            Debug.Log("Change to Med");
        }
        else
        {
            //high
            Debug.Log("Change to High");
        }
    }
}
