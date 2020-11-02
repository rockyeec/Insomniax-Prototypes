using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer(GameObject player, int curLevel, List<GameObject> diary)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, curLevel, diary);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Saved file");

            return data;
        }
        else
        {
            Debug.Log("Save file not found");
            return null;
        }
    }

    internal static void SavePlayer(SaveManager saveManager)
    {
        throw new NotImplementedException();
    }
}
