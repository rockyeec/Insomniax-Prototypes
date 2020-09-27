using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject DialogueElement;
    public RawImage characterIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<Dialogue.infomation> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    private void Start()
    {
        DialogueElement.SetActive(false);   
    }

    public void StartDialogue(Dialogue d)
    {
        DialogueElement.SetActive(true);
        Debug.Log("Start Dialogue");

        if(sentences != null)
        {
            sentences.Clear();
        }
        Debug.Log("Cleared previous dialogue");
        foreach(Dialogue.infomation i in d.dialogueInfo)
        {
            sentences.Enqueue(i);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue.infomation info = sentences.Dequeue();
        nameText.text = info.nameText;
    }

    public void EndDialogue()
    {
        //DialogueElement.SetActive(false);
    }
}
