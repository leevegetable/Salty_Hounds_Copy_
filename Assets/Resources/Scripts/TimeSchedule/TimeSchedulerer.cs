using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSchedulerer : MonoBehaviour
{
    public TimeScheduleManager.Type ManagerPath;

    public WeekSchedule[] weekSchedules = new WeekSchedule[0];
    [SerializeField]
    private TimeScheduleManager myScheduleManager = null;
    public int ID;
    private void Awake()
    {
        switch (gameObject.tag)
        {
            case "Map":
                ID = GetComponent<MapData>().MapCode;
                break;
            case "Character":
                ID = GetComponent<CharacterObject>().CharacterCode;
                break;
            case "Untagged":
                ID = 0;
                break;
        }
    }
    private void Start()
    {
        switch (ManagerPath)
        {
            case TimeScheduleManager.Type.global: myScheduleManager = TickbasePlaySystem.instance.GlobalSchedule; break;
            case TimeScheduleManager.Type.map: myScheduleManager = TickbasePlaySystem.instance.MapSchedule; break;
            case TimeScheduleManager.Type.character: myScheduleManager = TickbasePlaySystem.instance.CharacterSchedule; break;
        }
        myScheduleManager.AddSchedulerer(this);
    }

    public void UpdateWeek(int Week)
    {
        if (weekSchedules.Length == 0)
        {
            return;
        } 
        for(int i = 0; i < weekSchedules.Length; i++)
        {
            myScheduleManager.ScheduleMasterID.Add(ID);
            Debug.Log("UpdateWeek");
            weekSchedules[i].update(myScheduleManager,ID, Week);
        }
    }
}
