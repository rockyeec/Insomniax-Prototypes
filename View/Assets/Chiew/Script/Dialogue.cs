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
        public RawImage characImage;
        public string nameText;
        [TextArea(3, 10)]
        public string DialogueText;
        public bool isEndedSentence = false;
        public bool isPlayed = false;
    }
    public List<infomation> dialogueInfo;
}

