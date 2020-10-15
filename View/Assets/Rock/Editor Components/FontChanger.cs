using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class FontChanger : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] TMP_FontAsset font = null;
    [SerializeField] bool applyChanges = false;
    void Update()
    {
        if (!applyChanges)
            return;
        applyChanges = false;

        TextMeshProUGUI[] uguis = FindObjectsOfType<TextMeshProUGUI>();
        foreach (var item in uguis)
        {
            item.font = font;
        }

        TextMeshPro[] pros = FindObjectsOfType<TextMeshPro>();
        foreach (var item in pros)
        {
            item.font = font;
        }
    }
#endif
}
