using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonSpriteReplacer : MonoBehaviour
{
    [SerializeField] ColorBlock colors = ColorBlock.defaultColorBlock;
    [SerializeField] Color textColor = Color.black;
    [SerializeField] Sprite sprite = null;
    [SerializeField] bool applyChanges = false;

    // Update is called once per frame
    void Update()
    {
        if (!applyChanges)
            return;

        applyChanges = false;

        Button[] buttons = GetComponentsInChildren<Button>();
        if (buttons != null)
        {
            if (buttons.Length != 0)
            {               
                foreach (var item in buttons)
                {
                    item.colors = colors;

                    Image image = item.GetComponent<Image>();
                    image.sprite = sprite;

                    TextMeshProUGUI text = item.GetComponentInChildren<TextMeshProUGUI>();
                    text.color = textColor;
                }
            }
        }
    }
}
