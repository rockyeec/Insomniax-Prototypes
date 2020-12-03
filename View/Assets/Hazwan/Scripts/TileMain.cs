using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileMain : MonoBehaviour
{
    public GameObject messageBox;
    public TextMeshProUGUI textDialogue;

    public int indexRef;

    public string monologue = string.Empty;

    public Color yellow;

    public bool isInteracted = false;

    static TileMain instance;
    private void Awake()
    {
        instance = this;
    }
    List<GameObject> tileList = new List<GameObject>();

    public static List<GameObject> TileList { get { return instance.tileList; } }

    void Start()
    {
        messageBox.SetActive(false);
        SetTileIndex();
        TileGame.OnReset += TileGame_OnReset;
    }

    private void OnDestroy()
    {
        TileGame.OnReset -= TileGame_OnReset;
    }

    private void TileGame_OnReset()
    {
        isInteracted = false;
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("GroundCheck") && !isInteracted)
        {
            isInteracted = true;
            LeanTween.color(gameObject, yellow, 2f);
            TileGame.TileData.Add(indexRef);
            TileGame.totalInteractedTile++;
            TileGame.TileComparison();

            MonologueScript.TriggerText(monologue);

            AudioManager.instance.PlaySfx("stepTile");
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

}
