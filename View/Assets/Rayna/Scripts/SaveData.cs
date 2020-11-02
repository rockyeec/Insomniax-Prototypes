using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.RestService;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level;
    public List<GameObject> diaryContent = new List<GameObject>();
    public float[] position;


    public SaveData(GameObject player, int curLevel, List<GameObject> diary)
    {
        level = curLevel;

        for(int i =0; i<diary.Count; i++)
        {
            diaryContent.Add(diary[i]);
        }

        position = new float[3];
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
    }
}
