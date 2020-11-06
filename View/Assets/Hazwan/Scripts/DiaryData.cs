using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryData : MonoBehaviour
{
    #region Singleton

    private static DiaryData _instance;

    public static DiaryData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DiaryData>();
            }
            return _instance;
        }
    }
    #endregion

    public List<int> diarySequence = new List<int>();
    public List<GameObject> diaryEntries = new List<GameObject>();

    void Start()
    {
        diarySequence.Add(0);
        diarySequence.Add(1);
        diarySequence.Add(2);
    }


}
