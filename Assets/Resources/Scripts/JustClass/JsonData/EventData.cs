using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventData
{
    public int ID;
    public int NpcID;
    public DialogueData[] Dialogues;

    public EventData(int id, int npcID, DialogueData[] dialogue)
    {
        ID = id;
        NpcID = npcID;
        Dialogues = dialogue;
    }
}
