using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFlyManager : MonoBehaviour
{
    static HouseFlyManager instance;
    [SerializeField] GameObject[] housePrefabs = null;
    public static GameObject[] HousePrefabs { get { return instance.housePrefabs; } }

    [SerializeField] GameObject gameWorldRepresentationOfHousePrefab = null;
    [SerializeField] float fullLength = 20.0f;
    [SerializeField] int amount = 6;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokerForMonologue.Do("DisableGlasses");
        AudioManager.instance.PlayBgm("Main Music Glasses");
        float lengthBetween = fullLength / (amount - 1);
        float start = fullLength / 2.0f;
        for (int i = 0; i < amount; i++)
        {
            GameObject house = Instantiate(gameWorldRepresentationOfHousePrefab, transform);
            house.transform.localPosition = Vector3.zero.With(z: i * lengthBetween - start);
        }        
    }
}
