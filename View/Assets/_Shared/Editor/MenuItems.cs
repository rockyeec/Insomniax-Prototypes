using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOpeion()
    {
        PlayerPrefs.DeleteAll();
    }


}
