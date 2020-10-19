using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFlyManager : MonoBehaviour
{
    static HouseFlyManager instance;
    [SerializeField] GameObject[] housePrefabs = null;
    public static GameObject[] HousePrefabs { get { return instance.housePrefabs; } }

    [SerializeField] GameObject gameWorldRepresentationOfHousePrefab = null;
    [SerializeField] Transform[] rows = new Transform[2];
    [SerializeField] float fullLength = 20.0f;
    [SerializeField] int amount = 6;

    private void Awake()
    {
        instance = this;
        GameScript.OnGlassesOn += GameScript_OnGlassesOn;
        GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }
    private void OnDestroy()
    {
        GameScript.OnGlassesOn -= GameScript_OnGlassesOn;
        GameScript.OnGlassesOff -= GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        PlayerInput.MoveSpeed = 1.0f;
    }

    private void GameScript_OnGlassesOn()
    {
       
    }


    private void Start()
    {
        float lengthBetween = fullLength / (amount - 1);
        float start = fullLength / 2.0f;
        foreach (var row in rows)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject house = Instantiate(gameWorldRepresentationOfHousePrefab, row);
                house.transform.localPosition = new Vector3(i * lengthBetween - start, 0.0f, 0.0f);
            }
        }
    }
}
