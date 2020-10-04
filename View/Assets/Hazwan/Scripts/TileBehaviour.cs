using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileBehaviour : MonoBehaviour
{
    #region Singleton

    private static TileBehaviour _instance;

    public static TileBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameObject");
                go.AddComponent<TileBehaviour>();
            }
            return _instance;
        }
    }
    #endregion

    public static int totalOfGridX = 0;
    public static int totalOfGridY = 0;

    [HideInInspector]
    public List<GameObject> tile = new List<GameObject>();

    public List<int> leftTile = new List<int>();
    public List<int> rightTile = new List<int>();
    public List<int> startingTile = new List<int>();    // First row
    public List<int> middleTile = new List<int>();
    public List<int> endTile = new List<int>();
    public List<int> endEdgeTile = new List<int>();
    public List<int> startEdgeTile = new List<int>();

    int leftTileIndex = 0;
    int rightTileIndex = - 1;
    int startingTileIndex = 1;
    int endTileIndex = 1;
    int endEdgeTileIndex = 0;
    int startEdgeTileIndex = 0;

    bool stop = false;
    bool isPlayed = false;
    bool copyThis = true;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GameScript.OnGlassesOn += StartTileProgram;
    }

    private void StartTileProgram()
    {
        if(!isPlayed)
        {
            StartingTilesIndex();
            RightTilesIndex();
            LeftTilesIndex();
            EndTilesIndex();
            EndEdgesTileIndex();
            StartingEdgesTileIndex();
            MiddleTilesIndex();
            isPlayed = true;
        }
        
    }

    void Update()
    {
        if (Input.GetKey("up") && !stop)
        {
            StartingTilesIndex();
            RightTilesIndex();
            LeftTilesIndex();
            EndTilesIndex();
            EndEdgesTileIndex();
            StartingEdgesTileIndex();
            MiddleTilesIndex();
            
            stop = true;
        }

        if (Input.GetKey("down") && !stop)
        {
            EndTilesIndex();
            MiddleTilesIndex();
            stop = true;
        }
    }

    public void LeftTilesIndex()
    {
        for (int i = 0; i < totalOfGridY; i++)
        {
            leftTileIndex = totalOfGridX * i;
            leftTile.Add(leftTileIndex);
        }
    }

    public void RightTilesIndex()
    {
        for (int i = 0; i < totalOfGridY; i++)
        {
            rightTileIndex += totalOfGridX;
            rightTile.Add(rightTileIndex);
        }
    }

    public void StartingTilesIndex()
    {
        for (int i = 0; i < totalOfGridX - 2; i++)
        {
            startingTileIndex = i + 1;
            startingTile.Add(startingTileIndex);
        }
    }

    public void EndTilesIndex()
    {
        int y = tile.Count - totalOfGridX;

        for (int i = tile.Count; i > y + 2; i--)
        {
            endTileIndex = i - 2;
            endTile.Add(endTileIndex);
        }
    }

    public void EndEdgesTileIndex()
    {
        endEdgeTileIndex = tile.Count - 1;
        endEdgeTile.Add(endEdgeTileIndex);

        endEdgeTileIndex = tile.Count - totalOfGridX;
        endEdgeTile.Add(endEdgeTileIndex);

    }

    public void StartingEdgesTileIndex()
    {
        startEdgeTileIndex = 0;
        startEdgeTile.Add(startEdgeTileIndex);

        startEdgeTileIndex = totalOfGridX - 1;
        startEdgeTile.Add(startEdgeTileIndex);

    }

    public void MiddleTilesIndex()
    {
        for (int i = rightTile[0] + 2; i < tile.Count; i++)
        {
            for (int a = 0; a < leftTile.Count; a++)
            {
                if (i == leftTile[a])
                {
                    copyThis = false;
                    break;
                }
            }
            for (int a = 0; a < rightTile.Count; a++)
            {
                if (i == rightTile[a])
                {
                    copyThis = false;
                    break;
                }
            }

            for (int a = 0; a < endTile.Count; a++)
            {
                if(i == endTile[a])
                {
                    copyThis = false;
                    break;
                }
            }

            if (copyThis)
            {
                middleTile.Add(i);
            }

            copyThis = true;
        }
    }

    void Test()     // Explore this.
    {
        var b = rightTile.All(leftTile.Contains);

        Debug.Log(b);
        print(b);
    }
}
