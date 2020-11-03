using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    //public LevelManager levelManager;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this.gameObject);
    }

    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadPlayer();


        Debug.Log("Load in save manager " + data.level);
        SceneManager.LoadScene(data.level);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
    void Update()
    {
        if (this.gameObject.transform.position.y < -8.0f)
        {
            LoadPlayer();
        }
    }
}
