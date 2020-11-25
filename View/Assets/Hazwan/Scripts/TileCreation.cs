using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreation : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> tileList = new List<GameObject>();

    //public List<Transform> additionalObjectList = new List<Transform>();

    //rock---------------------------------------------------------
    [SerializeField] string[] additionalObjectMonologues = null;
    //-------------------------------------------------------------

    [SerializeField] GameObject[] additionalObject = null;

    [SerializeField] Vector3[] tilePos = new Vector3[5];

    private int totalTile = 5;
    private int tileCounter = 0;

    public GameObject tilePrefab;
    protected GameObject tile;
    
    void Start()
    {
        tilePrefab.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);

        for (int x = 0; x < totalTile; x++)
        {
            tile = Instantiate(tilePrefab, tilePos[x], Quaternion.identity);
            tile.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
            tile.name = string.Format("Tile-{0}", tileCounter);
            tileCounter++;
            tileList.Add(tile);

            //rock---------------------------------------------------------
            var tileMainScript = tile.GetComponent<TileMain>();
            tileMainScript.monologue = additionalObjectMonologues[x];
            //-------------------------------------------------------------
        }
        for (int i = 0; i < totalTile; i++)
        {
            additionalObject[i].transform.position = GetTilePos(i) + Vector3.up * 0.05f;
            additionalObject[i].transform.SetParent(tileList[i].transform);

            TileMain.TileList.Add(tileList[i]);
        }
    }

    Vector3 GetTilePos(int num)
    {
        Vector3 originalPos = new Vector3(tileList[num].transform.position.x, tileList[num].transform.position.y, tileList[num].transform.position.z);
        return originalPos;
    }
}
