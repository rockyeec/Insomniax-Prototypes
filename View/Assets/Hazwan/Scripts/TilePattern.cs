using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePattern : MonoBehaviour
{
    public GameObject tile;
    public int gridX = 5;
    public int gridY = 5;
    public float spacing = 2f;
    FallingTile[,] fallingTiles = null;

    void Start()
    {
        fallingTiles = new FallingTile[gridX, gridY];
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y) * spacing;
                fallingTiles[x, y] = Instantiate(tile, pos, Quaternion.identity).GetComponent<FallingTile>();
                if ((y == 0 && x == 2) || (y == 1 && x == 3) || (y == 2 && x == 2) || (y == 3 && x == 1) || (y == 4 && x == 0))
                {
                    fallingTiles[x, y].FakeTile(false);
                }
                else
                {
                    fallingTiles[x, y].FakeTile(true);
                }
            }
        }

        //GameScript.OnGlassesOff += GameScript_OnGlassesOff;
    }

    private void GameScript_OnGlassesOff()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                fallingTiles[x, y].FakeTile(Random.Range(0, 4) == 1);
            }
        }
    }
}
