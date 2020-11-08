using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    [System.Serializable]
    private class SaveData
    {
        public Dictionary<string, int> intDic = new Dictionary<string, int>();
        public Dictionary<string, bool> boolDic = new Dictionary<string, bool>();
        public Dictionary<string, float> floatDic = new Dictionary<string, float>();
        public Dictionary<string, string> stringDic = new Dictionary<string, string>();
        public Dictionary<string, float[]> vector3Dic = new Dictionary<string, float[]>();
    }

    private static void Save(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    private static SaveData Load()
    {
        string path = Application.persistentDataPath + "/player.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data;
            try
            {
                data = formatter.Deserialize(stream) as SaveData;
            }
            catch
            {
                data = new SaveData();
            }
            stream.Close();
            return data;
        }
        else
        {
            return new SaveData();
        }        
    }

   
    public static void Reset()
    {
        Save(new SaveData());
    }

    public static void SetInt(string key, int value)
    {
        SaveData data = Load();
        var dic = data.intDic;
        if (dic.ContainsKey(key))
        {
            dic[key] = value;
        }
        else
        {
            dic.Add(key, value);
        }

        Save(data);
    }
    public static int GetInt(string key)
    {
        SaveData data = Load();
        var dic = data.intDic;
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, 0);
        }
        return dic[key];
    }

    public static void SetBool(string key, bool value)
    {
        SaveData data = Load();

        var dic = data.boolDic;
        if (dic.ContainsKey(key))
        {
            dic[key] = value;
        }
        else
        {
            dic.Add(key, value);
        }

        Save(data);
    }
    public static bool GetBool(string key)
    {
        SaveData data = Load();
        var dic = data.boolDic;
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, false);
        }
        return dic[key];
    }
    
    public static void SetFloat(string key, float value)
    {
        SaveData data = Load();

        var dic = data.floatDic;
        if (dic.ContainsKey(key))
        {
            dic[key] = value;
        }
        else
        {
            dic.Add(key, value);
        }

        Save(data);
    }
    public static float GetFloat(string key)
    {
        SaveData data = Load();
        var dic = data.floatDic;
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, 0.0f);
        }
        return dic[key];
    }

    public static void SetString(string key, string value)
    {
        SaveData data = Load();

        var dic = data.stringDic;
        if (dic.ContainsKey(key))
        {
            dic[key] = value;
        }
        else
        {
            dic.Add(key, value);
        }

        Save(data);
    }
    public static string GetString(string key)
    {
        SaveData data = Load();
        var dic = data.stringDic;
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, string.Empty);
        }
        return dic[key];
    }

    public static void SetVector3(string key, Vector3 value)
    {
        SaveData data = Load();

        var dic = data.vector3Dic;
        float[] point = new float[3] { value.x, value.y, value.z };
        if (dic.ContainsKey(key))
        {
            dic[key] = point;
        }
        else
        {
            dic.Add(key, point);
        }

        Save(data);
    }
    public static Vector3 GetVector3(string key)
    {
        SaveData data = Load();
        var dic = data.vector3Dic;
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new float[3] { 0.0f, 0.0f, 0.0f });
        }
        return new Vector3( dic[key][0], dic[key][1], dic[key][2]);
    }
}
