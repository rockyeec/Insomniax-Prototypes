using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(transform.position, null);
    }

    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadData();

        SceneManager.LoadScene(data.level);

        if (data.position[0] == 0.0f && data.position[1] == 0.0f && data.position[2] == 0.0f)
            transform.position = new Vector3 ( data.position[0], data.position[1], data.position[2] );
    }
    void Update()
    {
        if (gameObject.transform.position.y < -8.0f)
        {
            LoadPlayer();
        }
    }
}
