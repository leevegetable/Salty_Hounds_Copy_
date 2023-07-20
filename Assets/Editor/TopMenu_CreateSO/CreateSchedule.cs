using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public static class CreateSchedule
{
    [MenuItem("TimeSchedule/CreateWeekSchedule", false, 11)]
    static void CreateWeekSchedule()
    {
        WeekSchedule temp = new WeekSchedule();
        AssetDatabase.CreateAsset(temp, SystemSetting.path_HourSchedule + "Week_0.asset");
    }
    [MenuItem("TimeSchedule/CreateDaySchedule", false, 12)]
    static void CreateDaySchedule()
    {
        DaySchedule temp = new DaySchedule();
        AssetDatabase.CreateAsset(temp, SystemSetting.path_HourSchedule + "Day_0.asset");
    }
    [MenuItem("TimeSchedule/CreateHourSchedule", false, 13)]
    static void CreateHourSchedule()
    {
        HourSchedule temp = new HourSchedule();
        AssetDatabase.CreateAsset(temp, SystemSetting.path_HourSchedule + "Time_0_00_to_0_00.asset");
    }
}
