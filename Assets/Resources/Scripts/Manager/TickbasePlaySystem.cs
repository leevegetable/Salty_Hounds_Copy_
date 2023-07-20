using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickbasePlaySystem : MonoBehaviour
{
    public static TickbasePlaySystem instance;
    public TimeScheduleManager GlobalSchedule;
    public TimeScheduleManager MapSchedule;
    public TimeScheduleManager CharacterSchedule;

    private int currentTick = 0;
    public int CurrentTick { get { return currentTick; } private set { currentTick = value; } }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        NextTick();
    }

    public void NextTick()
    {
        GlobalSchedule.UpdateWeek(0);
        GlobalSchedule.UpdateDay(0);
        MapSchedule.UpdateWeek(0);
        MapSchedule.UpdateDay(0);
        GlobalSchedule.ExecuteTick(CurrentTick);
        MapSchedule.ExecuteTick(CurrentTick);
        //CharacterSchedule.ExecuteTick(CurrentTick);
        CurrentTick++;
    }

    public void SetTick(int tick)
    {
        CurrentTick = tick;
    }
}
