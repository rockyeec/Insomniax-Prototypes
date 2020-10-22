using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTrigger : MonoBehaviour
{
    [SerializeField] ButtonsHighLighter manager = null;
    [SerializeField] Highlightable highlightable = null;
    [SerializeField] float delay = 0.0f;

    readonly string callAfterDelayString = "CallAfterDelay";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 20)
        {
            if (other.gameObject.name != "Player")
                return;

            Invoke(callAfterDelayString, delay);
        }
    }


    void CallAfterDelay()
    {
        manager.Highlight(highlightable);
        Destroy(transform.parent.gameObject);
    }
}
