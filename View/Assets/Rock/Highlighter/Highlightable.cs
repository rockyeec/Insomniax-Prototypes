using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private string description = "Drag To Move";
    public RectTransform RectTransform 
    { 
        get 
        {
            if (rect == null)
                rect = GetComponent<RectTransform>();
            return rect; 
        } 
    }
    public string Description { get { return description; } }
    
}
