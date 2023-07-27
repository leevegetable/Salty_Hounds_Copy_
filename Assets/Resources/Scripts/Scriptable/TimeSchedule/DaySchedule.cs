using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schedulerer.Task;
using JetBrains.Annotations;

public class DaySchedule : ScriptableObject
{
    public HourSchedule[] hourSchedules = new HourSchedule[0];

    public void update(TaskSchedulerer schedulerer, int id)
    {
        Debug.Log("UpdateDay");
        for (int i = 0; i < hourSchedules.Length; i++)
        {
            if (hourSchedules[i] == null) continue;
            int startTick = TickbasePlaySystem.getTimeToTick(hourSchedules[i].StartHour, hourSchedules[i].StartMinute);
            int endTick = TickbasePlaySystem.getTimeToTick(hourSchedules[i].EndHour, hourSchedules[i].EndMinute);
            schedulerer.Add(id, startTick, endTick, hourSchedules[i]);
        }
    }
}
