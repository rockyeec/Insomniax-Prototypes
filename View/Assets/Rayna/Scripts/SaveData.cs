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
    public List<GameObject> diaryCon;
    public float[] position;


    public SaveData(GameObject player)
    {
        level = LevelManager.CurrentLevel;

        //diaryCon = new List<GameObject>();
        for (int i = 0; i < Diary.diaryContent.Count; i++)
        {
            diaryCon.Add(Diary.diaryContent[i]);
            Debug.Log(diaryCon[i]);
        }

        position = new float[3];
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
    }
}
