using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    [SerializeField] private ButtonScript glassesButton = null;
    [SerializeField] private List<GlassesObject> glassesObjList = new List<GlassesObject>();


    private bool isGlassesOn = false;
    readonly private float lerpRate = 3.3f;

    private void Update()
    {
        if (glassesButton.IsDown)
        {
            isGlassesOn = !isGlassesOn;

            foreach (var item in glassesObjList)
            {
                if (isGlassesOn)
                {
                    item.Activate(in lerpRate);
                }
                else
                {
                    item.Deactivate(in lerpRate);
                }
            }
        }
    }
}
