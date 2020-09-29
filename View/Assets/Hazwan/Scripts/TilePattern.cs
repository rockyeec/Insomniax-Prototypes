using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePattern : MonoBehaviour
{
    #region Singleton

    private static TilePattern _instance;

    public static TilePattern Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameObject");
                go.AddComponent<TilePattern>();
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField]
    private GameObject tilePrefab = null;

    [HideInInspector]
    public List<GameObject> tileList = new List<GameObject>();

    protected GameObject tile;
    public int gridX = 5;
    public int gridY = 5;
    public float spacing = 2f; 

    private int tileCounter = 0;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, -5, y) * spacing;
                tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.name = string.Format("Tile-{0}", tileCounter);
                tileCounter++;
                
                tileList.Add(tile);
                TileBehaviour.Instance.tile.Add(tile);
            }
        }
    }

    void Update()
    {
        //tileList[2].transform.position = new Vector3(tileList[2].transform.position.x, 0, tileList[2].transform.position.z);
        TileBehaviour.totalOfGridX = gridX;
        TilePlay.grid_X = gridX;
        TileBehaviour.totalOfGridY = gridY;
        TilePlay.grid_Y = gridY;
    }


}
