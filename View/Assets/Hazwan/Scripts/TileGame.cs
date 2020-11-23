using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGame : MonoBehaviour
{
    public static int totalInteractedTile = 0;

    static int[] correctSequence = { 0, 1, 2, 3, 4, 5 };

    public static List<int> tileData = new List<int>();

    [SerializeField] Color redColor;
    [SerializeField] Color greenColor;

    //rock----------------------------------------------------
    [SerializeField] GameObject towardsFogMonologue = null;
    //--------------------------------------------------------

    private static TileGame instance;
    private void Awake()
    {
        instance = this;
    }
    
    public static void TileComparison()
    {
        if (totalInteractedTile != 6)
            return;

        for (int i = 0; i < TileMain.TileList.Count; i++)
        {
            if(correctSequence[i] == tileData[i])
            {
                if(i == TileMain.TileList.Count - 1)
                {
                    instance.CheckSequence(true);
                }
            }
            else
            {
                instance.CheckSequence(false);
                return;
            }
        }
    }

    public static void SetTileColor(Color c)
    {
        for (int i = 0; i < TileMain.TileList.Count; i++)
        {
            TileMain.TileList[i].GetComponent<MeshRenderer>().material.color = c;
        }
    }

    public void CheckSequence(bool isCorrect)
    {
        if(isCorrect)
            StartCoroutine(CorrectSequence());
        else
            StartCoroutine(IncorrectSequence());
    }

    private IEnumerator CorrectSequence()
    {
        //rock-----------------------------------------------------------------------
        yield return new WaitForSeconds(0.2f);
        MonologueScript.TriggerText("That seemed to have done something, but what?");
        Destroy(towardsFogMonologue);
        //---------------------------------------------------------------------------

        CameraFollowTileGame.Instance.isCorrect = true;
        yield return new WaitForSeconds(2f);
        CameraFollowTileGame.Instance.changeCameraView = true;
        yield return new WaitForSeconds(2f);
        SetTileColor(instance.greenColor);
    }

    private IEnumerator IncorrectSequence()
    {
        //rock-----------------------------------------------------------------------
        yield return new WaitForSeconds(0.2f);
        MonologueScript.TriggerText(new string[2] {
                "What just happened? Did I do something wrong?", 
                "Looks like I have to start again." });
        //---------------------------------------------------------------------------

        totalInteractedTile = 0;
        yield return new WaitForSeconds(6f);
        CameraFollowTileGame.Instance.changeCameraView = true;
        yield return new WaitForSeconds(2f);
        SetTileColor(instance.redColor);
        yield return new WaitForSeconds(3f);
        GameScript.TakeOffGlasses();
        GameScript.PutOnGlasses();
    }
}
