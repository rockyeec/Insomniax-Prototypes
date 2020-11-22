using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileGame
{
    List<int> myIntList = new List<int>();

    public void Init()
    {
        myIntList.Clear();

        int[] myIntArr = SaveSystem.GetIntArr("intArr");
        if (myIntArr != null)
            myIntList.AddRange(myIntArr);
    }

    public void SavedArray(int i)
    {
        myIntList.Add(i);
        int[] myIntArr = myIntList.ToArray();
        SaveSystem.SetIntArr("intArr", myIntArr);
        foreach (var item in SaveSystem.GetIntArr("intArr"))
        {
            Debug.Log(item);
        }

        /*for (int j = 0; j < myIntArr.Length; j++)
        {
            var item = SaveSystem.GetIntArr("intArr")[j];
            Debug.Log(item);
        }*/

        /*bool isEqual = myIntArr.SequenceEqual(myIntList); // <- USE THIS FOR COMPARISON [NOTE TO HAZWAN] && TRY USING EVENT

        if(SaveSystem.GetIntArr("intArr").SequenceEqual(myIntList))
        {
            // may proceed 
        }
        else
        {
            // restart
        }*/
    }
}
