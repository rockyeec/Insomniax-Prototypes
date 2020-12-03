using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TileGame : MonoBehaviour
{
    int totalInteractedTile = 0;
    public static int TotalInteractedTile { get { return instance.totalInteractedTile; } set { instance.totalInteractedTile = value; } }

    static int[] correctSequence = { 0, 1, 2, 3, 4 };

    List<int> tileData = new List<int>();
    public static List<int> TileData{ get { return instance.tileData; } }

    public Color redColor;
    public Color greenColor;
    public Color origin;

    public static event Action OnReset = delegate { };
    public static event Action<Color> OnSetColor = delegate { };

    //rock----------------------------------------------------
    [SerializeField] GameObject towardsFogMonologue = null;
    //--------------------------------------------------------

    public static TileGame instance;
    private void Awake()
    {
        instance = this;
        TotalInteractedTile = 0;
    }

    public static void TileComparison()
    {
        print(TotalInteractedTile);
        if (TotalInteractedTile != 5)
            return;

        /*for (int i = 0; i < TileMain.TileList.Count; i++)
        {
            if(correctSequence[i] == TileData[i])
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
        }*/
        instance.CheckSequence(correctSequence.SequenceEqual(TileData));
    }

    public void CheckSequence(bool isCorrect)
    {
        InvokerForMonologue.Do("DisableMoveControl");
        InvokerForMonologue.Do("DisableCameraControl");
        InvokerForMonologue.Do("DisableJump");
        InvokerForMonologue.Do("DisableDiary");
        if (isCorrect)
            StartCoroutine(CorrectSequence());
        else
            StartCoroutine(IncorrectSequence());
    }

    public void Restart()
    {
        StartCoroutine(RestartLevel());
    }

    private IEnumerator CorrectSequence()
    {
        //rock-----------------------------------------------------------------------
        yield return new WaitForSeconds(3.0f);
        MonologueScript.TriggerText("That seemed to have done something, but what?");
        Destroy(towardsFogMonologue);
        //---------------------------------------------------------------------------

        CameraFollowTileGame.Instance.isCorrect = true;
        yield return new WaitForSeconds(2f);
        CameraFollowTileGame.Instance.changeCameraView = true;
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySfx("tileCorrect");
        OnSetColor(instance.greenColor);
        yield return new WaitForSeconds(8.2f);
        InvokerForMonologue.Do("EnableCameraControl");
        InvokerForMonologue.Do("EnableJump");
        InvokerForMonologue.Do("EnableDiary");
        InvokerForMonologue.Do("EnableMoveControl");
        yield return new WaitForSeconds(2f);
        EntryPrompt.Instance.PromptActivation(9);
    }

    private IEnumerator IncorrectSequence()
    {
        TotalInteractedTile = 0;
        TileData.Clear();
        yield return new WaitForSeconds(3f);
        CameraFollowTileGame.Instance.changeCameraView = true;
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlaySfx("tileWrong");
        OnSetColor(instance.redColor);
        
        //rock-----------------------------------------------------------------------
        yield return new WaitForSeconds(1.5f);
        MonologueScript.TriggerText(new string[2] {
                "What just happened? Did I do something wrong?",
                "Looks like I have to start again." });
        //---------------------------------------------------------------------------
        yield return new WaitForSeconds(3f);
        OnSetColor(instance.origin);
        GameScript.TakeOffGlasses();
        GameScript.PutOnGlasses();

        yield return new WaitForSeconds(3f);
        OnReset();
        InvokerForMonologue.Do("EnableCameraControl");
        InvokerForMonologue.Do("EnableJump");
        InvokerForMonologue.Do("EnableDiary");
        InvokerForMonologue.Do("EnableMoveControl");
    }

    private IEnumerator RestartLevel()
    {
        TotalInteractedTile = 0;
        TileData.Clear();
        OnSetColor(instance.origin);
        yield return new WaitForSeconds(2f);
        GameScript.TakeOffGlasses();
        GameScript.PutOnGlasses();

        //rock-----------------------------------------------------------------------
        yield return new WaitForSeconds(2f);
        MonologueScript.TriggerText(new string[2] {
                "I need to do all over again?",
                "Hm" });
        //---------------------------------------------------------------------------

        yield return new WaitForSeconds(3f);
        OnReset();
    }
}
