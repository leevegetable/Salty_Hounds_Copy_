using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Schedulerer.Task;

public class TimeScheduleManager : MonoBehaviour
{
    private delegate void schedulerers(int tick);

    private schedulerers timeSchedulerers;

    public List<DaySchedule[]> EveryWeekSchedule = new List<DaySchedule[]>();
    public List<DaySchedule[]> DelayWeekSchedule = new List<DaySchedule[]>();
    public List<DaySchedule[]> OneoffWeekSchedule = new List<DaySchedule[]>();

    public TaskSchedulerer TodaySchedule = new TaskSchedulerer();

    private void Start()
    {
        
    }

    public void AddSchedulerer(TimeSchedulerer schedulerer)
    {
        timeSchedulerers += schedulerer.UpdateWeek;
    }

    public void UpdateWeek(int week)
    {
        ClearWeekSchedule();
        ClearTodayTask();
        timeSchedulerers(week);
    }

    public void UpdateDay(int day)
    {
        updateDay(EveryWeekSchedule, TodaySchedule, day);
        updateDay(DelayWeekSchedule, TodaySchedule, day);
        updateDay(OneoffWeekSchedule, TodaySchedule, day);
    }

    public void ExecuteTick(int tick)
    {
        TodaySchedule.Execute(tick);
    }

    private void updateDay(List<DaySchedule[]> weekschedule, TaskSchedulerer taskSchedulerer, int day)
    {
        for (int i = 0; i < weekschedule.Count; i++)
        {
            if (weekschedule[i][day] == null) continue;
            weekschedule[i][day].update(taskSchedulerer, i);
        }
    }

    private void ClearWeekSchedule()
    {
        EveryWeekSchedule.Clear();
        DelayWeekSchedule.Clear();
        OneoffWeekSchedule.Clear();
    }

    private void ClearTodayTask()
    {
        TodaySchedule.Clear();
    }

    public static int getTimeToTick(int hour, int sM)
    {
        return ((hour * 60) + sM) / 5;
    }
    public static int[] getTickToTime(int tick)
    {
        int hour = tick / 60;
        int minute = tick - (hour * 60);
        return new int[2] { hour, minute };
    }
}
