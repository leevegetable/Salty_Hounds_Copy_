using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int FileIndex = -1;
    public int Week = 1;
    public int Day = 0;
    public int Hour = 0;
    public int Minute = 0;
    public int[] CharacterFeelings = new int[SystemSetting.count_MainActors];
    public int[] PlayerStats = new int[SystemSetting.count_Stats];
    public int MapID = 0;
    public SaveData()
    {
        FileIndex = -1;
        Week = 1;
        Day = 0;
        Hour = 0;
        Minute = 0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileIndex">���̺����� �ε���</param>
    /// <param name="week">�� ���� �ΰ�</param>
    /// <param name="day">�� �����ΰ� 0���� ������</param>
    /// <param name="hour">�� ���ΰ�</param>
    /// <param name="minute">�� ���ΰ�</param>
    public SaveData(int fileIndex, int week, int day, int hour, int minute)
    {
        FileIndex = fileIndex;
        Week = week;
        Day = day;
        Hour = hour;
        Minute = minute;
    }

    public void UpdateData()
    {
    
    }
}
