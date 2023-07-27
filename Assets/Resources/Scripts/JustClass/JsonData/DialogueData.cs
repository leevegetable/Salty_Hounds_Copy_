using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public int ID;
    public DialogueManager.DialoguePath SubPath;
    public Dialog[] Dialogues;

    public DialogueData(int id, Dialog[] dialogue)
    {
        ID = id;
        Dialogues = dialogue;
    }
}
