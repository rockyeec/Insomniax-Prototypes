using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class infomation
    {
        public Sprite characImage;
        public string nameText;
        [TextArea(3, 10)]
        public string DialogueText;
        public bool isEndedSentence = false;
        public bool isPlayed = false;
        public bool isQuestion = false;
        public MCQInfo[] newMCQ = new MCQInfo[3]; //Line 25
    }
    public List<infomation> dialogueInfo;
}

[System.Serializable]
public class MCQInfo //MCQ Information Class
{
    public string answerString;
    public Dialogue nxtDialogue;
}