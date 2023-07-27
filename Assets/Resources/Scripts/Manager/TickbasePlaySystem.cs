using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickbasePlaySystem : MonoBehaviour
{
    public static TickbasePlaySystem instance;
    public TimeScheduleManager GlobalSchedule;
    public TimeScheduleManager MapSchedule;
    public TimeScheduleManager CharacterSchedule;
    public List<GameEvent> EventOrderList = new List<GameEvent>();
    public List<GameEvent> CurrentPlayingList = new List<GameEvent>();
    public List<GameEvent> WaitingEventList = new List<GameEvent>();
    [SerializeField]
    private int currentWeek = 0;

    [SerializeField]
    private int currentDay = 0;

    [SerializeField]
    private int currentTick = 286;
    public int CurrentWeek { get { return currentWeek; } }
    public int CurrentDay { get { return currentDay; } }
    public int CurrentTick { get { return currentTick; } private set { currentTick = value; } }

    private void Awake()
    {
        instance = this;
    }
    private delegate bool booltest();
    private booltest Booltest;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (currentWeek < 0)
        //        NextWeek();
        //    if (currentDay < 0)
        //        NextDay();
        //    ExecuteTick();

        //}
    }

    public void ExecuteTick()
    {
        if (EventOrderList.Count == 0) return;
        while (EventOrderList.Count > 0)
        {
                Debug.Log("Excute");
            CurrentPlayingList.Add(EventOrderList[0]);
            EventOrderList.Remove(EventOrderList[0]);
            StartCoroutine(Coroutine_Execute(CurrentPlayingList[CurrentPlayingList.Count - 1]));
        }
        UIManager.instance.playState.Refresh();
    }

    private IEnumerator Coroutine_Execute(GameEvent gameEvent)
    {
        int actionIndex = 0;
        bool currentIndexisRunning = false;
        while (actionIndex < gameEvent.EventActions.Count)
        {
            yield return null;
            if (!currentIndexisRunning)
            {
                currentIndexisRunning = true;
                StartCoroutine(gameEvent.EventActions[actionIndex].Execute(gameEvent));
            }
            else
            {
                if (gameEvent.EventActions[actionIndex].actionEnd || gameEvent.EventActions[actionIndex].isOverapNextAction)
                {
                    actionIndex++;
                    currentIndexisRunning = false;
                }
            }
        }
        yield break;
    }

    public void NextWeek()
    {
        currentWeek++;
        weekScheduleUpdate();
        UIManager.instance.playState.Refresh();
    }

    public void weekScheduleUpdate()
    {
        GlobalSchedule.UpdateWeek(currentWeek);
        MapSchedule.UpdateWeek(currentWeek);
    }

    public void NextDay()
    {
        if (currentDay < 7)
        {
            currentDay++;
        }
        else
        {
            currentDay = 0;
            NextWeek();
        }
        dayScheduleUpdate();
        UIManager.instance.playState.Refresh();
    }

    public void dayScheduleUpdate()
    {
        GlobalSchedule.UpdateDay(currentDay);
        MapSchedule.UpdateDay(currentDay, PlayerManager.instance.autoSave.MapID);
    }

    public void NextTick()
    {
        if (CurrentTick < 287)
        {
            CurrentTick++;
        }
        else
        {
            CurrentTick = 0;
            NextDay();
        }
        TickSchedueUpdate();
        if (PlayerManager.instance.controller.isControl)
        {
            ExecuteTick();
        }
        UIManager.instance.playState.Refresh();
    }

    private void TickSchedueUpdate()
    {
        MapSchedule.UpdateDay(currentDay, PlayerManager.instance.autoSave.MapID);
        GlobalSchedule.UpdateTick(CurrentTick);
        MapSchedule.UpdateTick(CurrentTick);
    }

    public void SkipTick()
    {
        if (CurrentTick < 287)
        {
            CurrentTick++;
        }
        else
        {
            CurrentTick = 0;
            NextDay();
        }
        UIManager.instance.playState.Refresh();
    }

    public void SetTime(int week, int day, int hour, int minute)
    {
        currentWeek = week;
        weekScheduleUpdate();
        currentDay = day;
        dayScheduleUpdate();
        currentTick = getTimeToTick(hour,minute);
        TickSchedueUpdate();
        UIManager.instance.playState.Refresh();
    }

    public void AddPlayingEvent(GameEvent gameEvent)
    {
        if(!EventOrderList.Contains(gameEvent))
            EventOrderList.Add(gameEvent);
    }

    public void EndEvent(GameEvent gameEvent)
    {
        if (CurrentPlayingList.Contains(gameEvent))
        {
            CurrentPlayingList.Remove(gameEvent);
            if (WaitingEventList.Count != 0 && isPlayingEvent())
            {
                for (int i = 0; i < WaitingEventList.Count; i++)
                {
                    WaitingEventList[i].Update();
                    if (!WaitingEventList[i].AllowOverap)
                        return;
                }
            }
        }
    }

    public void AddWaitingEvent(GameEvent gameEvent)
    {
        if (!WaitingEventList.Contains(gameEvent))
        {
            WaitingEventList.Add(gameEvent);
        }
    }

    public bool isPlayingEvent()
    {
        if (EventOrderList.Count == 0) return true;
        else
        {
            for(int i = 0; i < EventOrderList.Count; i++)
            {
                if (EventOrderList[i].AllowOverap) return false;
            }
            return true;
        }
    }

    public static int getTimeToTick(int hour, int sM)
    {
        return ((hour * 60) + sM) / 5;
    }
    public static int[] getTickToTime(int tick)
    {
        int tickTominute = tick * 5;
        int hour = tickTominute / 60;
        int minute = tickTominute - (hour * 60);
        return new int[2] { hour, minute };
    }
}
