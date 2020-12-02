using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvokerForMonologue : MonoBehaviour
{
    [SerializeField] ButtonsHighLighter manager = null;
    [SerializeField] Highlightable glassesButton = null;
    [SerializeField] Highlightable moveButton = null;
    [SerializeField] Highlightable lookButton = null;
    [SerializeField] Highlightable jumpButton = null;
    [SerializeField] Highlightable menuButton = null;
    [SerializeField] Highlightable fakeDiaryButton = null;

    [SerializeField] ButtonEnabler glassesEnabler = null;
    [SerializeField] ButtonEnabler moveEnabler = null;
    [SerializeField] ButtonEnabler lookEnabler = null;
    [SerializeField] ButtonEnabler jumpEnabler = null;
    [SerializeField] ButtonEnabler menuEnabler = null;

    [SerializeField] Camera3rdPerson cam = null;
    [SerializeField] GlassesController glassesController = null;

    readonly List<string> commands = new List<string>();

    private static InvokerForMonologue instance;
    public static bool IsHold { get; set; }

    protected virtual void Awake()
    {
        instance = this;
        IsHold = false;
        Add("DisableCameraControl");
        Add("EnableCameraControl");
        Add("DisableMoveControl");
        Add("EnableMoveControl");
        Add("HighlightGlasses");
        Add("HighlightMove");
        Add("HighlightLook");
        Add("HighlightJump");
        Add("HighlightMenu");
        Add("HighlightDiary");
        Add("TurnOffHighlight");
        Add("Hold");
        Add("SlowMove");
        Add("NormalMove");

        Add("EnableGlasses");
        Add("DisableGlasses");
        Add("EnableJump");
        Add("DisableJump");
        Add("EnableMenu");
        Add("DisableMenu");
        Add("EnableDiary");
        Add("DisableDiary");

        Add("SnapOutMove");
        Add("SnapOutGlasses");
        Add("SnapOutJump");
        Add("ShakeScreen");
        Add("BlurScreen");
        Add("ClearScreen");

        Add("SetGlassesOn");
        Add("SetGlassesOff");

        Add("UnlockEntry");
    }

    private void Add(in string command)
    {
        commands.Add(command);
    }

    public static bool ContainsCommand(in string command)
    {
        return instance.commands.Contains(command);
    }

    public static void Do(in string command)
    {
        instance.Invoke(command, 0.0f);
    }

    void DisableCameraControl()
    {
        PlayerInput.IsEnableCamera = false;
        lookEnabler.SlideOut();
    }
    void EnableCameraControl()
    {
        PlayerInput.IsEnableCamera = true;
        lookEnabler.SlideIn();
    }

    void DisableMoveControl()
    {
        PlayerInput.IsCanMove = false;
        moveEnabler.SlideOut();
    }
    void EnableMoveControl()
    {
        PlayerInput.IsCanMove = true;
        moveEnabler.SlideIn();
    }
    void SlowMove()
    {
        PlayerInput.MoveSpeed = 0.5f;
    }
    void NormalMove()
    {
        PlayerInput.MoveSpeed = 1.0f;
    }
    void HighlightGlasses()
    {
        manager.Highlight(glassesButton);
    }
    void HighlightMove()
    {
        manager.Highlight(moveButton);
    }
    void HighlightLook()
    {
        manager.Highlight(lookButton);
    }
    void HighlightJump()
    {
        manager.Highlight(jumpButton);
    }
    void HighlightMenu()
    {
        manager.Highlight(menuButton);
    }
    void HighlightDiary()
    {
        manager.Highlight(fakeDiaryButton);
    }
    void TurnOffHighlight()
    {
        manager.TurnOff();
    }
    void Hold()
    {
        IsHold = true;
    }

    void EnableGlasses()
    {
        glassesEnabler.GetComponent<Image>().raycastTarget = true;
        glassesEnabler.SlideIn();
    }
    void DisableGlasses()
    {
        glassesEnabler.GetComponent<Image>().raycastTarget = false;
        glassesEnabler.SlideOut();
    }

    void EnableJump()
    {
        jumpEnabler.SlideIn();
    }
    void DisableJump()
    {
        jumpEnabler.SlideOut();
    }

    void EnableMenu()
    {
        menuEnabler.SlideIn();
    }
    void DisableMenu()
    {
        menuEnabler.SlideOut();
    }

    void EnableDiary()
    {
        OpenDiaryBtn.Instance.gameObject.SetActive(true);
    }
    void DisableDiary()
    {
        OpenDiaryBtn.Instance.gameObject.SetActive(false);
    }

    void SnapOutMove()
    {
        moveEnabler.SnapOut();
    }
    void SnapOutGlasses()
    {        
        glassesEnabler.SnapOut();
    }
    void SnapOutJump()
    {
        jumpEnabler.SnapOut();
    }
    void ShakeScreen()
    {
        cam.Vibrate();
    }
    void BlurScreen()
    {
        AudioManager.instance.PlaySfx("Whoosh");
        CameraBlurer.Blur();
    }
    void ClearScreen()
    {
        AudioManager.instance.PlaySfx("Whoosh");
        CameraBlurer.Clear();
    }
    void SetGlassesOn()
    {
        SaveSystem.SetBool("is glasses", true);
        glassesController.LoadState();
    }
    void SetGlassesOff()
    {
        SaveSystem.SetBool("is glasses", false);
        glassesController.LoadState();
    }

    void UnlockEntry()
    {
        int curEntry = 6 + SaveSystem.GetInt("invoker entry");
        SaveSystem.SetInt("invoker entry", curEntry - 5); // increment entry

        EntryPrompt.Instance.PromptActivation(curEntry);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HighlightGlasses();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            HighlightMove();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            HighlightLook();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            HighlightJump();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            HighlightMenu();
        }
    }*/
}
