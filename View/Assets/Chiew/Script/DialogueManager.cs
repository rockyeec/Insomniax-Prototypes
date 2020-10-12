using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Public Variable Here Please
    public static DialogueManager instance;

    [Header("Elements")]
    public GameObject DialogueElement;
    public Image characterIcon;
    public TextMeshProUGUI nameText;
    public TextMeshPro nameTextWorld;
    public TextMeshProUGUI dialogueText;
    public TextMeshPro dialogueTextWorld;
    public GameObject[] NextbuttonParents = new GameObject[2];
    public GameObject Controls;

    [Header("Buttons For Answer")]
    public GameObject answerButtonParent;
    public Button[] answerButtons = new Button[3];

    [HideInInspector]
    public bool isDialogueDone = true;

    //Private Variable Below Please
    private Queue<Dialogue.infomation> sentences = new Queue<Dialogue.infomation>();
    private MCQInfo[] MCQTemp = new MCQInfo[3];
    private bool isSentenceEnd = false; //is the sentence end
    private int curSentenceLength, MaxSentenceLength; //Calculate the current sentence length and max length
    private string tempStore; //Replace if press next before show up

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
        Controls.SetActive(true);
    }

    public void StartDialogue(Dialogue d) //Call start dialogue
    {
        DialogueElement.SetActive(true);
        Controls.SetActive(false);
        Debug.Log("Start Dialogue");
        isDialogueDone = false;
        sentences.Clear();

        Debug.Log("Cleared previous dialogue");
        foreach(Dialogue.infomation i in d.dialogueInfo)
        {
            sentences.Enqueue(i);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() //Display more sentence
    {
        curSentenceLength = dialogueTextWorld.text.Length; //determine current length value
        if (isSentenceEnd == true)
        {
            curSentenceLength = 0;
            MaxSentenceLength = 0;
            isSentenceEnd = false;
        } //Reset default value
        if (curSentenceLength != 0 && MaxSentenceLength != 0)
        {
            StopAllCoroutines();
            if (curSentenceLength >= MaxSentenceLength)
            {
                dialogueTextWorld.text = tempStore;
                isSentenceEnd = true;
            }
            if (curSentenceLength < MaxSentenceLength)
            {
                dialogueTextWorld.text = tempStore;
                isSentenceEnd = true;
                return;
            }
        } //If current got sentence playing ald
        if (sentences.Count <= 0) //End dialogue
        {
            EndDialogue(); //end dialogue if no more dialogues
            return;
        }
        StopAllCoroutines();
        dialogueTextWorld.text = ""; dialogueText.text = ""; tempStore = ""; //Reset all word
        Dialogue.infomation info = sentences.Dequeue();
        tempStore = info.DialogueText;
        StartCoroutine(Wordbyword(info.DialogueText)); //Animation display
        //dialogueText.text = info.DialogueText; //This is for normal display
        //characterIcon.sprite = info.characImage; //If needed picture/icon
        characterIcon.gameObject.SetActive(false); //If doesn't want picture/icon
        if(nameText.gameObject != null)
        {
            nameText.text = info.nameText; //Display name
        }
        if(nameTextWorld.gameObject != null)
        { nameTextWorld.text = info.nameText; }

        //MCQ
        AssignAnswerToButton(info);
    }

    void AssignAnswerToButton(Dialogue.infomation curInfo) //Assign text to buttons.text
    {
        if (curInfo.isQuestion == true)
        {
            foreach (Button i in answerButtons) { i.gameObject.SetActive(true); }
            foreach (GameObject i in NextbuttonParents) { i.SetActive(false); }
            for (int assign = 0; assign < answerButtons.Length; assign++)
            {
                if (curInfo.newMCQ[assign] != null && curInfo.newMCQ[assign].answerString != "")
                { answerButtons[assign].GetComponentInChildren<TextMeshProUGUI>().text = curInfo.newMCQ[assign].answerString; }
                else { answerButtons[assign].GetComponentInChildren<TextMeshProUGUI>().text = null; answerButtons[assign].gameObject.SetActive(false); }

                MCQTemp[assign] = curInfo.newMCQ[assign];
            }
        }
        else
        {
            foreach (GameObject i in NextbuttonParents) { i.SetActive(true); }
            foreach (Button i in answerButtons) { i.gameObject.SetActive(false); }
        }
    }

    public void OnPressButtonSelection(int i) //For Buttons
    {
        if(MCQTemp[i] != null && MCQTemp[i].nxtDialogue != null)
        {
            StartDialogue(MCQTemp[i].nxtDialogue);
            return;
        }

        EndDialogue(); return;
    }

    IEnumerator Wordbyword (string sentence) //Animation show words one by one for dialogue text
    {
        MaxSentenceLength = sentence.Length;
        if (dialogueText.gameObject != null)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;              
            }
        }

        if(dialogueTextWorld.gameObject != null)
        {
            dialogueTextWorld.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueTextWorld.text += letter;
                yield return null;
            }
        }
    }

    public void EndDialogue() //End Dialogues
    {
        for(int i = 0; i < MCQTemp.Length; i++)
        {
            MCQTemp[i] = null;
        }
        DialogueElement.SetActive(false);
        Controls.SetActive(true);
        isDialogueDone = true;
    }
}