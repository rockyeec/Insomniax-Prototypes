using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void Start()
    {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SetVector3("player position", transform.position);
    }

    public void LoadPlayer()
    {
        Vector3 position = SaveSystem.GetVector3("player position");

        if (position == Vector3.zero)
            return;

        transform.position = position;
    }
}
