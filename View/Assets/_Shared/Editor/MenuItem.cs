using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/Clear Binary Save")]
    private static void ClearBinarySave()
    {
        SaveSystem.Reset();
    }
}