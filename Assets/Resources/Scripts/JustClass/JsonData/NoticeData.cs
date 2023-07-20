using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoticeData
{
    public int NoticeLevel = 0;
    public string[] Messages;

    public NoticeData()
    {
        NoticeLevel = 0;
        Messages = new string[0];
    }

    public NoticeData(int noticeLevel, string[] messages)
    {
        NoticeLevel = noticeLevel;
        Messages = messages;
    }

}
