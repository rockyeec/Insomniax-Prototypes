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

    int lowestArea = -25;

    MeshRenderer rend;

    void Start()
    {
        
        rend = GetComponent<MeshRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;

        for (int i = 0; i < TileBehaviour.Instance.tile.Count; i++)
        {
            CallTile.Add(TileBehaviour.Instance.tile[i]);
        }

        CallTile[testing].transform.position = new Vector3(CallTile[testing].transform.position.x, 0, CallTile[testing].transform.position.z);
        CallTile[testing2].transform.position = new Vector3(CallTile[testing2].transform.position.x, 0, CallTile[testing2].transform.position.z);
        CallTile[testing3].transform.position = new Vector3(CallTile[testing3].transform.position.x, 0, CallTile[testing3].transform.position.z);

        SetTileIndex();
    }

    

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        startFadingIn();
        print(CallTile[indexRef]);

        for (int i = 1; i < TileBehaviour.Instance.leftTile.Count - 1; i++)
        {
            int x = TileBehaviour.Instance.leftTile[i];

            if(gameObject == CallTile[x])
            {
                LeftEdgePattern(x);
                break;
            }
        }

        for (int i = 1; i < TileBehaviour.Instance.rightTile.Count - 1; i++)
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

        for (int i = 0; i < grid_X - 2; i++)
        {
            int x = TileBehaviour.Instance.endTile[i];
            if (gameObject == CallTile[x])
            {
                print("Touched");
                EndPattern(x);
                break;
            }
        }

        if (gameObject == CallTile[TileBehaviour.Instance.endEdgeTile[1]])
        {
            int x = TileBehaviour.Instance.endEdgeTile[1];
            EndEdgeLeftPattern(x);
        }

        if (gameObject == CallTile[TileBehaviour.Instance.endEdgeTile[0]])
        {
            int x = TileBehaviour.Instance.endEdgeTile[0];
            EndEdgeRightPattern(x);
        }

        if (gameObject == CallTile[TileBehaviour.Instance.startEdgeTile[0]])
        {
            int x = TileBehaviour.Instance.startEdgeTile[0];
            StartEdgeLeftPattern(x);
        }

        if (gameObject == CallTile[TileBehaviour.Instance.startEdgeTile[1]])
        {
            int x = TileBehaviour.Instance.startEdgeTile[1];
            StartEdgeRightPattern(x);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        startFadingOut();
    }

    void StartingPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef;
        tempTile[4] = tileRef + grid_X;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
            }
        }
    }

    void EndPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef - grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void MiddlePattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
            }
        }

    }

    void LeftEdgePattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef + 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
            }
        }
    }

    void RightEdgePattern(int tileRef)
    {
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef - 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void EndEdgeLeftPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef + 1;
        tempTile[2] = tileRef - grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
            }
        }
    }

    void EndEdgeRightPattern(int tileRef)
    {
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef - grid_X].transform.position = new Vector3(CallTile[tileRef - grid_X].transform.position.x, 0, CallTile[tileRef - grid_X].transform.position.z);

        tempTile[0] = tileRef - 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef - grid_X;
        tempTile[3] = tileRef - grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void StartEdgeLeftPattern(int tileRef)
    {
        CallTile[tileRef + 1].transform.position = new Vector3(CallTile[tileRef + 1].transform.position.x, 0, CallTile[tileRef + 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);

        tempTile[0] = tileRef + 1;
        tempTile[1] = tileRef + 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef + grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
                print(CallTile[i]);
            }
        }
    }

    void StartEdgeRightPattern(int tileRef)
    {
        CallTile[tileRef - 1].transform.position = new Vector3(CallTile[tileRef - 1].transform.position.x, 0, CallTile[tileRef - 1].transform.position.z);
        CallTile[tileRef + grid_X].transform.position = new Vector3(CallTile[tileRef + grid_X].transform.position.x, 0, CallTile[tileRef + grid_X].transform.position.z);

        tempTile[0] = tileRef - 1;
        tempTile[1] = tileRef - 1;
        tempTile[2] = tileRef + grid_X;
        tempTile[3] = tileRef + grid_X;
        tempTile[4] = tileRef;

        for (int i = 0; i < CallTile.Count; i++)
        {
            if (i != tempTile[0] && i != tempTile[1] && i != tempTile[2] && i != tempTile[3] && i != tempTile[4])
            {
                CallTile[i].transform.position = new Vector3(CallTile[i].transform.position.x, lowestArea, CallTile[i].transform.position.z);
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

    IEnumerator FadeIn()
    {
        for (float f = 0; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startFadingIn()
    {
        StartCoroutine(FadeIn());
    }

    public void startFadingOut()
    {
        StartCoroutine(FadeOut());
    }
}
