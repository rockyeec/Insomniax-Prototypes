using System.Collections.Generic;
using UnityEngine;

public class InvokerForMonologue : MonoBehaviour
{
    readonly List<string> commands = new List<string>();

    private static InvokerForMonologue instance;
    private void Awake()
    {
        instance = this;
        commands.Add("DisableCameraControl");
        commands.Add("EnableCameraControl");
        commands.Add("DisableMoveControl");
        commands.Add("EnableMoveControl");
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
}
