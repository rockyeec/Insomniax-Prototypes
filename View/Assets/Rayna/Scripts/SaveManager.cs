using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //public LevelManager levelManager;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this.gameObject, LevelManager.CurrentLevel, Diary.diaryContent);
    }

    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
}
