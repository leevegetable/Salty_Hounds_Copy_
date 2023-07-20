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
        for (int i = 0; i < hourSchedules.Length; i++)
        {
            if (hourSchedules[i] == null) continue;
            int startTick = TimeScheduleManager.getTimeToTick(hourSchedules[i].StartHour, hourSchedules[i].StartMinute);
            int endTick = TimeScheduleManager.getTimeToTick(hourSchedules[i].EndHour, hourSchedules[i].EndMinute);
            schedulerer.Add(id, startTick, endTick, hourSchedules[i]);
        }
    }
}
