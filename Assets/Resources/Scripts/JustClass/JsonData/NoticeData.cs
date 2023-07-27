using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoticeData
{
    public int ID;
    public int NoticeLevel = 0;
    public string Messages;

    public NoticeData()
    {
        NoticeLevel = 0;
        Messages = "";
    }

    public NoticeData(int noticeLevel, string messages)
    {
        NoticeLevel = noticeLevel;
        Messages = messages;
    }

}
