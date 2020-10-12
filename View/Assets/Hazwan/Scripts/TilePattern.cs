using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public List<Transform> camTilePos = new List<Transform>(); 


    public List<TextMeshPro> tileDialogue = new List<TextMeshPro>(); 


    protected GameObject tile;
    public int gridX = 5;
    public int gridY = 5;
    public float spacing = 2f; 

    private int tileCounter = 0;

    public Transform camTiles;
    public TextMeshPro textDialogue;

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
                Vector3 pos = new Vector3(x, -10, y) * spacing;
                tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.name = string.Format("Tile-{0}", tileCounter);
                tileCounter++;
                tileList.Add(tile);
                TileBehaviour.Instance.tile.Add(tile);
            }
        }

        for (int i = 0; i < tileList.Count; i++)
        {
            camTiles = tileList[i].gameObject.transform.GetChild(0);
            camTilePos.Add(camTiles);

            textDialogue = tileList[i].gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
            tileDialogue.Add(textDialogue);

            TilePlay.ChildTilePos.Add(camTilePos[i]);

            TilePlay.DialogueTextlist.Add(tileDialogue[i]);
        }
    }

    void Update()
    {
        TileBehaviour.totalOfGridX = gridX;
        TilePlay.grid_X = TileBehaviour.totalOfGridX;
        TileBehaviour.totalOfGridY = gridY;
        TilePlay.grid_Y = gridY;
        TileDialogueScript.SetDialogueScript();

    }
}
