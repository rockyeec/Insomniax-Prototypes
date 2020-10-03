using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject DialogueElement;
    public Image characterIcon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<Dialogue.infomation> sentences = new Queue<Dialogue.infomation>();

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

    public void StartDialogue(Dialogue d) //Call start dialogue
    {
        DialogueElement.SetActive(true);
        Debug.Log("Start Dialogue");

        sentences.Clear();

        Debug.Log("Cleared previous dialogue");
        foreach(Dialogue.infomation i in d.dialogueInfo)
        {
            sentences.Enqueue(i);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            EndDialogue(); //end dialogue if no more dialogues
            return;
        }

        Dialogue.infomation info = sentences.Dequeue();
        nameText.text = info.nameText; //Display name
        StartCoroutine(Wordbyword(info.DialogueText)); //Animation display
        //dialogueText.text = info.DialogueText; //This is for normal display
    }

    IEnumerator Wordbyword (string sentence) //Animation show words one by one for dialogue text
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        DialogueElement.SetActive(false);
    }
}
