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
        QualitySettings.SetQualityLevel(num);
    }
}
