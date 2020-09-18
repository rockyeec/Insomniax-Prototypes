using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePattern : MonoBehaviour
{
    public GameObject Tile;
    public GameObject FakeTile;
    public float gridX = 5f;
    public float gridY = 5f;
    public float spacing = 2f;

    void Start()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y) * spacing;
                if((y == 0 && x == 2) || (y == 1 && x == 3) || (y == 2 && x == 2) || (y == 3 && x == 1) || (y == 4 && x == 0))
                {
                    Instantiate(Tile, pos, Quaternion.identity);
                }
                else
                {
                    Instantiate(FakeTile, pos, Quaternion.identity);
                }
            }
        }
    }
}
