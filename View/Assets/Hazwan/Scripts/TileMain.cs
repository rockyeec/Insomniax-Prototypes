using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileMain : MonoBehaviour
{
    public TextMeshProUGUI textDialogue;

    public int indexRef;

    [SerializeField] string monologue = string.Empty;

    public Color yellow;

    public bool isInteracted = false;

    static TileMain instance;
    private void Awake()
    {
        instance = this;
    }
    List<GameObject> tileList = new List<GameObject>();

    Renderer rend = null;

    public static List<GameObject> TileList { get { return instance.tileList; } }

    void Start()
    {
        SetTileIndex();
        TileGame.OnSetColor += TileGame_OnSetColor;
        TileGame.OnReset += TileGame_OnReset;
    }
    private void OnDestroy()
    {
        TileGame.OnSetColor -= TileGame_OnSetColor;
        TileGame.OnReset -= TileGame_OnReset;
    }


    private void TileGame_OnSetColor(Color c)
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        rend.material.color = c;
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
            TileGame.TotalInteractedTile++;
            TileGame.TileComparison();

            MonologueScript.TriggerText(monologue);

            AudioManager.instance.PlaySfx("stepTile");
        }
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
