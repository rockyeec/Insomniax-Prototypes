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
        [Header("Bubble or UI")]
        public bool isBubble = false;
        [Header("Change Text Color")]
        public Color TextColor = new Color(255,255,255,255);
        [Header("Picture/Sprite")]
        public Sprite characImage;
        [Header("Name And Dialogues")]
        public string nameText;
        [TextArea(3, 10)]
        public string DialogueText;
        [HideInInspector]
        public bool isEndedSentence = false;
        [HideInInspector]
        public bool isPlayed = false;

        [Header("If only have MCQ")]
        public bool isQuestion = false;
        [Header("Maximum 3")]
        public MCQInfo[] newMCQ = new MCQInfo[3]; //Line 25

        [Header("For Level 3 dialogue, tick for last selection")]
        public bool isEndMCQ = false;
    }
    public List<infomation> dialogueInfo;
}

[System.Serializable]
public class MCQInfo //MCQ Information Class
{
    public string answerString;
    public Dialogue nxtDialogue;
}