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
    public int curEnergy = 1;
    public int maxEnergy = 70;
    public int[] CharacterFeelings = new int[SystemSetting.count_MainActors];
    public int[] PlayerStats = new int[SystemSetting.count_Stats];
    public int Gold = 0;
    public int MapID = 0;
    public SaveData()
    {
        FileIndex = -1;
        Week = 1;
        Day = 0;
        Hour = 0;
        Minute = 0;
        curEnergy = 1;
        maxEnergy = 70;
        CharacterFeelings = new int[SystemSetting.count_MainActors];
        PlayerStats = new int[SystemSetting.count_Stats];
        Gold = 0;
        MapID = 0;
}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileIndex">세이브파일 인덱스</param>
    /// <param name="week">몇 주차 인가</param>
    /// <param name="day">몇 요일인가 0부터 월요일</param>
    /// <param name="hour">몇 시인가</param>
    /// <param name="minute">몇 분인가</param>
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
