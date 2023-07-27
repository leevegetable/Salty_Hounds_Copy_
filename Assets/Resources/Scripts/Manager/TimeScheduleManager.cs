using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Schedulerer.Task;

public class TimeScheduleManager : MonoBehaviour
{
    public enum Type { global, map, character }

    public Type type;

    private delegate void schedulerers(int tick);

    private schedulerers timeSchedulerers;
    public List<int> ScheduleMasterID = new List<int>();
    public Dictionary<int,DaySchedule[]> EveryWeekSchedule = new Dictionary<int, DaySchedule[]>();
    public Dictionary<int,DaySchedule[]> DelayWeekSchedule = new Dictionary<int, DaySchedule[]>();
    public Dictionary<int,DaySchedule[]> OneoffWeekSchedule = new Dictionary<int, DaySchedule[]>();

    public TaskSchedulerer TodaySchedule = new TaskSchedulerer();

    public void AddSchedulerer(TimeSchedulerer schedulerer)
    {
        Debug.Log("Added Schedulerer");
        timeSchedulerers += schedulerer.UpdateWeek;
    }

    public void UpdateWeek(int week)
    {
        ClearWeekSchedule();
        ClearTodayTask();
        if (timeSchedulerers == null)
        {
            Debug.Log(gameObject);
            return;
        } 
        timeSchedulerers(week);
    }

    public void UpdateDay(int day)
    {
        ClearTodayTask();
        updateDay(EveryWeekSchedule, TodaySchedule, day);
        updateDay(DelayWeekSchedule, TodaySchedule, day);
        updateDay(OneoffWeekSchedule, TodaySchedule, day);
    }

    public void UpdateDay(int day, int targetID)
    {
        ClearTodayTask();
        if (ScheduleMasterID.Contains(targetID))
        {
            int IDindex = ScheduleMasterID.IndexOf(targetID);
            if(EveryWeekSchedule.ContainsKey(ScheduleMasterID[IDindex]) && EveryWeekSchedule[targetID][day] != null)
                updateDay(EveryWeekSchedule[targetID][day], TodaySchedule, targetID);
            if (DelayWeekSchedule.ContainsKey(ScheduleMasterID[IDindex]) && EveryWeekSchedule[targetID][day] != null)
                updateDay(DelayWeekSchedule[targetID][day], TodaySchedule, targetID);
            if (OneoffWeekSchedule.ContainsKey(ScheduleMasterID[IDindex]) && EveryWeekSchedule[targetID][day] != null)
                updateDay(OneoffWeekSchedule[targetID][day], TodaySchedule, targetID);
        }

    }

    public void UpdateTick(int tick)
    {
        TodaySchedule.Update(tick);
    }

    private void updateDay(DaySchedule weekschedule, TaskSchedulerer taskSchedulerer, int mapCode)
    {

        if (weekschedule == null) return;
        weekschedule.update(taskSchedulerer, mapCode);
    }

    private void updateDay(Dictionary<int,DaySchedule[]> weekschedule, TaskSchedulerer taskSchedulerer, int day)
    {
        for (int i = 0; i < ScheduleMasterID.Count; i++)
        {
            if (weekschedule.ContainsKey(ScheduleMasterID[i]) ||weekschedule[ScheduleMasterID[i]][day] == null) continue;
            weekschedule[ScheduleMasterID[i]][day].update(taskSchedulerer, i);
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


}
