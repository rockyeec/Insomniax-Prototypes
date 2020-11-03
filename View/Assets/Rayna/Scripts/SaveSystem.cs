
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Vector3 position, int? level)
    {

        SaveData data = LoadData();
        if (data == null)
        {
            data = new SaveData();
        }

        data.level = level ?? data.level;
        data.position = new float[3] { position.x, position.y, position.z };


        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/player.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            //Debug.Log("Saved file");

            return data;
        }
        else
        {
            //Debug.Log("Save file not found");
            return null;
        }
        
    }
}
