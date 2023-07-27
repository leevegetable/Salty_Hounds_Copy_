using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WeekSchedule : ScriptableObject
{
    [TextArea] public string Description = "주차별 옵션이 있으리라 생각되어 일단 만들었습니다. 경영 요소가 있다보니 시간적 엔딩이 있지 않을까 추정하였습니다.";
    [Header("스케줄이 실행될 주차")]
    public int Week = -1;
    [Header("매 주 고정 스케줄")]
    public bool EveryWeek = false;
    [Header("n 주에 한 번 씩")]
    public int DelayWeek = 0;
    [Header("0부터 월화수목금토일")]
    public DaySchedule[] FixedDaySchedules = new DaySchedule[7];

    public void update(TimeScheduleManager manager,int id,int week)
    {
        if (Week != week && !isDelayWeek(week))
        {
            Debug.Log(Week + " , " + week);
            return;
        } 
        if (EveryWeek)
        {
            AddWeekSchedule(manager.EveryWeekSchedule, id);
        }
        else if (isDelayWeek(week))
        {
            AddWeekSchedule(manager.DelayWeekSchedule, id);
        }
        else
        {
            AddWeekSchedule(manager.OneoffWeekSchedule, id);
        }

    }
    private void AddWeekSchedule(Dictionary<int, DaySchedule[]> WeekSchedule, int ID)
    {
        if (WeekSchedule.ContainsKey(ID))
            WeekSchedule[ID] = FixedDaySchedules;
        else
            WeekSchedule.Add(ID, FixedDaySchedules);
    }

    private bool isDelayWeek(int week)
    {
        if (DelayWeek < 0)
            return false;
        if ((float)(week - Week) % (float)DelayWeek == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
