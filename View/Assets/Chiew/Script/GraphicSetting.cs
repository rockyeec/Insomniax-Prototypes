using TMPro;
using UnityEngine;

public class GraphicSetting : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown = null;

    // Start is called before the first frame update
    void Start()
    {
        DefaultQuality();  
    }

    public void DefaultQuality()
    {
        if (!PlayerPrefs.HasKey("graphicsSetting"))
            PlayerPrefs.SetInt("graphicsSetting", 1);

        dropdown.value = PlayerPrefs.GetInt("graphicsSetting");
        GraphicSettingButtons(PlayerPrefs.GetInt("graphicsSetting"));
    }

    public void GraphicSettingButtons(int num)
    {
        PlayerPrefs.SetInt("graphicsSetting", num);
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
