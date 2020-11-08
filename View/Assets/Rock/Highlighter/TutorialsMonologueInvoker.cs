/*using UnityEngine;
using UnityEngine.UI;

public class TutorialsMonologueInvoker : InvokerForMonologue
{
    [SerializeField] ButtonsHighLighter manager = null;
    [SerializeField] Highlightable glassesButton = null;

    bool isGlassesHighlighted = false;
    protected override void Awake()
    {
        base.Awake();
        Add("HighlightGlasses");

        glassesButton.GetComponent<Button>().onClick.AddListener(MakeCanMoveAgain);
    }
   
    private void HighlightGlasses()
    {
        manager.Highlight(glassesButton);
        isGlassesHighlighted = true;
    }
    private void MakeCanMoveAgain()
    {
        if (isGlassesHighlighted)
        {
            PlayerInput.IsCanMove = true;
            isGlassesHighlighted = false;
        }
    }
}
*/