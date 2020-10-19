using System.Collections.Generic;
using UnityEngine;

public class InvokerForMonologue : MonoBehaviour
{
    readonly List<string> commands = new List<string>();

    protected static InvokerForMonologue Instance { get; set; }

    protected virtual void Awake()
    {
        Instance = this;
        commands.Add("DisableCameraControl");
        commands.Add("EnableCameraControl");
        commands.Add("DisableMoveControl");
        commands.Add("EnableMoveControl");
    }

    protected void Add(in string command)
    {
        commands.Add(command);
    }

    public static bool ContainsCommand(in string command)
    {
        return Instance.commands.Contains(command);
    }

    public static void Do(in string command)
    {
        Instance.Invoke(command, 0.0f);
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
}
