using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public int NpcID;
    public int NpcFace;
    public int EventCGID;
    public string[] Line;
    public Dialog()
    {
        NpcFace = 0;
        EventCGID = -1;
    }
    public Dialog(int face, int eventCGID, string[] line)
    {
        NpcFace = face;
        EventCGID = eventCGID;
        Line = line;
    }
}
