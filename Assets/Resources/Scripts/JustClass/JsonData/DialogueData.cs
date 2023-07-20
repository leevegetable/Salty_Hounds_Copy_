using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public int NpcFace;
    public int EventCGID;
    public int SelectOptionID;
    public string[] Line;
    public DialogueData()
    {
        NpcFace = 0;
        EventCGID = -1;
        SelectOptionID = -1;
}
    public DialogueData(int face, int eventCGID, string[] line,  int selectOptionID)
    {
        NpcFace = face;
        EventCGID = eventCGID;
        Line = line;
        SelectOptionID = selectOptionID;
    }
}
