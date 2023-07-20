using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSchedulerer : MonoBehaviour
{
    public enum Type {global, map, character }
    public Type type;
    public WeekSchedule[] weekSchedules = new WeekSchedule[0];
    private TimeScheduleManager myScheduleManager = null;

    private void Start()
    {
        switch (type)
        {
            case Type.global: myScheduleManager = TickbasePlaySystem.instance.GlobalSchedule; break;
            case Type.map: myScheduleManager = TickbasePlaySystem.instance.MapSchedule; break;
            case Type.character: myScheduleManager = TickbasePlaySystem.instance.CharacterSchedule; break;
        }
        myScheduleManager.AddSchedulerer(this);
    }

    public void UpdateWeek(int Week)
    {
        for(int i = 0; i < weekSchedules.Length; i++)
        {
            Debug.Log("UpdateWeek");
            weekSchedules[i].update(myScheduleManager, Week);
        }
    }
}
