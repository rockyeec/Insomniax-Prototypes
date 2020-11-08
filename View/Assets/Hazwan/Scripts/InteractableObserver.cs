using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObserver : MonoBehaviour
{
    int index;

    public static event System.Action<int> OnInteracted = delegate { };

    public void Init(int i)
    {
        index = i;
    }

    public void Trigger()
    {
        OnInteracted(index);
    }
}
