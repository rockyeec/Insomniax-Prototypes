using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SavePlayer()
    {
        SaveSystem.SetVector3("player position", transform.position);
    }

    public void LoadPlayer()
    {
        transform.position = SaveSystem.GetVector3("player position");
    }
    /*void Update()
    {
        if (gameObject.transform.position.y < -8.0f)
        {
            LoadPlayer();
        }
    }*/
}
