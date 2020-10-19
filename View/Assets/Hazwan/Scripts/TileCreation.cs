using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileCreation : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> tileList = new List<GameObject>();

    public List<Transform> additionalObjectList = new List<Transform>();

    [SerializeField]
    private Transform additionalObject = null;

    public GameObject tilePrefab;
    protected GameObject tile;
    public float gridX = 5f;
    public float gridY = 5f;
    public float spacing = 2f;
    public float height = 1;
    private int tileCounter = 0;

    void Start()
    {
        tilePrefab.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);

        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, height, y) * spacing;
                tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                tile.name = string.Format("Tile-{0}", tileCounter);
                tileCounter++;
                tileList.Add(tile);
            }
        }

        for (int i = 0; i < tileList.Count; i++)
        {
            additionalObject = tileList[i].gameObject.transform.GetChild(0);
            additionalObjectList.Add(additionalObject);

            TileMain.TileList.Add(tileList[i]);
        }
    }

    void Update()
    {
        
    }
}
