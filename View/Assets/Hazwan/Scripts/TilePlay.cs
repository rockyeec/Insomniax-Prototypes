using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlay : TilePattern
{
    public List<GameObject> CallTile = new List<GameObject>();
    public List<GameObject> LeftTiles = new List<GameObject>();

    public static int grid_X = 0;
    public static int grid_Y = 0;

    int[] tempTile = new int[5];

    public int indexRef;

    int testing = 0;
    int testing2 = 6;
    int testing3 = 3;

    //public bool isMiddle = true;

    //public static int totalOfLeftTiles = 0;

    void Start()
    {
        for (int i = 0; i < TileBehaviour.Instance.tile.Count; i++)
        {
            CallTile.Add(TileBehaviour.Instance.tile[i]);
        }

        CallTile[testing].transform.position = new Vector3(CallTile[testing].transform.position.x, 0, CallTile[testing].transform.position.z);
        CallTile[testing2].transform.position = new Vector3(CallTile[testing2].transform.position.x, 0, CallTile[testing2].transform.position.z);
        CallTile[testing3].transform.position = new Vector3(CallTile[testing3].transform.position.x, 0, CallTile[testing3].transform.position.z);

        SetTileIndex();

        //TileBehaviour.Instance.RightTilesIndex();

        
    }

    void Update()
    {
        /*for (int i = 0; i < TileBehaviour.Instance.leftTile.Count; i++)
        {
            int x = TileBehaviour.Instance.leftTile[i];

            if (gameObject == CallTile[x])
            {
                isMiddle = false;
                break;
            }
        }

        for (int i = 0; i < TileBehaviour.Instance.rightTile.Count; i++)
        {
            int x = TileBehaviour.Instance.rightTile[i];

            if (gameObject == CallTile[x])
            {
                isMiddle = false;
                break;
            }
        }*/
    }

    private void OnTriggerEnter(Collider collision)
    {

        print(CallTile[indexRef]);

        for (int i = 0; i < TileBehaviour.Instance.leftTile.Count; i++)
        {
            int x = TileBehaviour.Instance.leftTile[i];

            if(gameObject == CallTile[x])
            {
                LeftEdgePattern(x);
                break;
            }
        }

        for (int i = 0; i < TileBehaviour.Instance.rightTile.Count; i++)
        {
            int x = TileBehaviour.Instance.rightTile[i];

            if (gameObject == CallTile[x])
            {
                RightEdgePattern(x);
                break;
            }
        }

        for (int i = 0; i < TileBehaviour.Instance.startingTile.Count; i++)
        {
            int x = TileBehaviour.Instance.startingTile[i];

            if (gameObject == CallTile[x])
            {
                StartingPattern(x);
                break;
            }
        }

        for (int i = 0; i < TileBehaviour.Instance.middleTile.Count; i++)
        {
            int x = TileBehaviour.Instance.middleTile[i];

            if (gameObject == CallTile[x])
            {
                MiddlePattern(x);
                break;
            }
        }

    }

    void StartingPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_Y].transform.position = new Vector3(CallTile[tileRef + grid_Y].transform.position.x, 0, CallTile[tileRef + grid_Y].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_Y;
        tempTile[3] = tileRef;
        tempTile[4] = tileRef + grid_Y;

        print(tempTile[0]);
        print(tempTile[1]);
        print(tempTile[2]);
        print(tempTile[3]);
        print(tempTile[4]);

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, -5, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void EndPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef - grid_Y].transform.position = new Vector3(CallTile[tileRef - grid_Y].transform.position.x, 0, CallTile[tileRef - grid_Y].transform.position.z);


    }

    void MiddlePattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_Y].transform.position = new Vector3(CallTile[tileRef + grid_Y].transform.position.x, 0, CallTile[tileRef + grid_Y].transform.position.z);
        CallTile[tileRef - grid_Y].transform.position = new Vector3(CallTile[tileRef - grid_Y].transform.position.x, 0, CallTile[tileRef - grid_Y].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_Y;
        tempTile[3] = tileRef - grid_Y;
        tempTile[4] = tileRef;

        print(tempTile[0]);
        print(tempTile[1]);
        print(tempTile[2]);
        print(tempTile[3]);
        print(tempTile[4]);

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, -5, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }

    }

    void LeftEdgePattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef + grid_Y].transform.position = new Vector3(CallTile[tileRef + grid_Y].transform.position.x, 0, CallTile[tileRef + grid_Y].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef + 1;
        tempTile[2] = tileRef + grid_Y;
        tempTile[3] = tileRef + grid_Y;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, -5, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void RightEdgePattern(int tileRef)
    {
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_Y].transform.position = new Vector3(CallTile[tileRef + grid_Y].transform.position.x, 0, CallTile[tileRef + grid_Y].transform.position.z);

        tempTile[0] = tileRef - 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_Y;
        tempTile[3] = tileRef + grid_Y;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, -5, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void SetTileIndex()
    {
        for (int i = 0; i < CallTile.Count; i++)
        {
            if (gameObject == CallTile[i])
            {
                indexRef = i;
            }
        }

    }

}
