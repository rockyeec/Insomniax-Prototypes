using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GraphicSetting : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropdown = null;

    [SerializeField]
    UniversalRenderPipelineAsset urp = null;

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

        switch (num)
        {
            case 0: //! low
                urp.shadowCascadeOption = ShadowCascadesOption.NoCascades;
                urp.shadowDistance = 5.0f;
                break;


            case 1: //! medium
                urp.shadowCascadeOption = ShadowCascadesOption.TwoCascades;
                urp.shadowDistance = 10.0f;
                break;


            case 2: //! high
                urp.shadowCascadeOption = ShadowCascadesOption.FourCascades;
                urp.shadowDistance = 25.0f;
                break;
        }
    }
}
