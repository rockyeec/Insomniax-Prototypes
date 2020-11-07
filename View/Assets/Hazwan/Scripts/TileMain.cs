using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileMain : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI textDialogue;

    public int indexRef;

    public static List<GameObject> TileList = new List<GameObject>();

    void Start()
    {
        messageBox.SetActive(false);
        SetTileIndex();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("GroundCheck"))
        {
            SetMemories();
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
