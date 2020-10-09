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

    [Header("Buttons For Answer")]
    public GameObject answerButtonParent;
    public Button[] answerButtons = new Button[3];

    [HideInInspector]
    public bool isDialogueDone = true;

    //Private Variable Below Please
    private Queue<Dialogue.infomation> sentences = new Queue<Dialogue.infomation>();
    private MCQInfo[] MCQTemp = new MCQInfo[3];

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
        isDialogueDone = false;
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
        characterIcon.sprite = info.characImage;
        if(nameText.gameObject != null)
        {
            nameText.text = info.nameText; //Display name
        }
        if(nameTextWorld.gameObject != null)
        { nameTextWorld.text = info.nameText; }
        StartCoroutine(Wordbyword(info.DialogueText)); //Animation display
        //dialogueText.text = info.DialogueText; //This is for normal display

        //MCQ
        AssignAnswerToButton(info);
    }

    void AssignAnswerToButton(Dialogue.infomation curInfo)
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
            foreach (Button i in answerButtons){ i.gameObject.SetActive(false);}
        }
    }

    public void OnPressButtonSelection(int i)
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
        if(dialogueText.gameObject != null)
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
        isDialogueDone = true;
    }
}