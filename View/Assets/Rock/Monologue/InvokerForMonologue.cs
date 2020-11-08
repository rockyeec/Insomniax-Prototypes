using System.Collections.Generic;
using UnityEngine;

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
        Add("EnableMove");
        Add("DisableMove");
        Add("EnableLook");
        Add("DisableLook");
        Add("EnableJump");
        Add("DisableJump");
        Add("EnableMenu");
        Add("DisableMenu");
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
    }
    void EnableCameraControl()
    {
        PlayerInput.IsEnableCamera = true;
    }

    void DisableMoveControl()
    {
        PlayerInput.IsCanMove = false;
    }
    void EnableMoveControl()
    {
        PlayerInput.IsCanMove = true;
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
        glassesEnabler.SlideIn();
    }
    void DisableGlasses()
    {
        glassesEnabler.SlideOut();
    }

    void EnableMove()
    {
        moveEnabler.SlideIn();
    }
    void DisableMove()
    {
        moveEnabler.SlideOut();
    }

    void EnableLook()
    {
        lookEnabler.SlideIn();
    }
    void DisableLook()
    {
        lookEnabler.SlideOut();
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
