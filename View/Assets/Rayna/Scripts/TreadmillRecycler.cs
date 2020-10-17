using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TreadmillRecycler : MonoBehaviour
{
    Vector3 spawnPoint;

    [SerializeField] float platformLength = 45.0f;
    [SerializeField] float speed = 0.12f;

    [SerializeField] bool applyChanges = false;

    TreadmillScript[] allTreadmills;

    static public Vector3 SpawnPoint { get { return instance.spawnPoint; } }
    static public float Speed { get { return instance.speed; } }
    static public float KillSelfZ { get; private set; }

    static TreadmillRecycler instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!applyChanges)
            return;
        applyChanges = false;

        allTreadmills = GetComponentsInChildren<TreadmillScript>();

        KillSelfZ = -platformLength * allTreadmills.Length / 2;
        spawnPoint = new Vector3(0.0f, 0.0f, platformLength * allTreadmills.Length / 2);

        for (int i = 0; i < allTreadmills.Length; i++)
        {
            allTreadmills[i].Art.localScale
                = allTreadmills[i].Box.size
                = new Vector3(6.9f, 1.0f, platformLength);

            allTreadmills[i].transform.localPosition = new Vector3(
                0.0f,
                allTreadmills[i].transform.localPosition.y,
                platformLength * allTreadmills.Length / 2 - platformLength * i);
        }
    }
}
