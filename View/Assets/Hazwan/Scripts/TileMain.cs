using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileMain : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI textDialogue;

    public int indexRef;

    [SerializeField] Color yellow;

    bool isInteracted = false;

    public static List<GameObject> TileList = new List<GameObject>();

    void Start()
    {
        messageBox.SetActive(false);
        SetTileIndex();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("GroundCheck") && !isInteracted)
        {
            isInteracted = true;
            //SetMemories();
            LeanTween.color(gameObject, yellow, 2f);
            TileGame.tileData.Add(indexRef);
            TileGame.totalInteractedTile++;
            TileGame.TileComparison();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        messageBox.SetActive(false);
    }

    void SetTileIndex()
    {
        for (int i = 0; i < TileList.Count; i++)
        {
            if (gameObject == TileList[i])
            {
                indexRef = i;
            }
        }
    }

    void SetMemories()
    {
        if (indexRef == 1)
        {
            print("Text changed.");
            messageBox.SetActive(true);
            textDialogue.text = "Working 1";
        }
        else if (indexRef == 2)
        {
            print("Text changed.");
            messageBox.SetActive(true);
            textDialogue.text = "Working 2";
        }
        else
        {
            print("Text changed.");
            messageBox.SetActive(true);
            textDialogue.text = "Empty";
        }
    }
}
