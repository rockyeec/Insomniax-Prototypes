using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] position;
    

    public SaveData(GameObject player)
    {
        position = new float[3];
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
    }
}
