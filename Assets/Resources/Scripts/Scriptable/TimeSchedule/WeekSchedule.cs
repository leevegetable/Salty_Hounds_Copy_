using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class WeekSchedule : ScriptableObject
{
    [TextArea] public string Description = "������ �ɼ��� �������� �����Ǿ� �ϴ� ��������ϴ�. �濵 ��Ұ� �ִٺ��� �ð��� ������ ���� ������ �����Ͽ����ϴ�.";
    [Header("�������� ����� ����")]
    public int Week = -1;
    [Header("�� �� ���� ������")]
    public bool EveryWeek = false;
    [Header("n �ֿ� �� �� ��")]
    public int DelayWeek = 0;
    [Header("0���� ��ȭ���������")]
    public DaySchedule[] FixedDaySchedules = new DaySchedule[7];

    public void update(TimeScheduleManager manager,int week)
    {
        if (Week != week && !isDelayWeek(week))
        {
            Debug.Log(Week + " , " + week);
            return;
        } 
        if (EveryWeek)
        {
            manager.EveryWeekSchedule.Add(FixedDaySchedules);
        }
        else if (isDelayWeek(week))
        {
            manager.DelayWeekSchedule.Add(FixedDaySchedules);
        }
        else
        {
            manager.OneoffWeekSchedule.Add(FixedDaySchedules);
        }

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
