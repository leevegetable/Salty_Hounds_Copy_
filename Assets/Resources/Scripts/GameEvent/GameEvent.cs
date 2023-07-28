using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName =("assetstest"),menuName =("createGameData"),order = 0)]
public class GameEvent : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField] private SystemSetting.WorkerName WorkerName;
    [SerializeField] public List<string> TagList = new List<string>();
#endif
    public enum Type {Global, Map, Character }
    public Type type;
    [SerializeField] private int eventID = 0;
    [SerializeField] public bool AllowOverap = false;

    [SerializeField] private bool useCharacterFeelings;
    [SerializeField] private int[] needCharacterFeelings = new int[SystemSetting.count_MainActors];

    [SerializeField] private bool useStats;
    [SerializeField] private int[] needStats = new int[SystemSetting.count_Stats];

    [SerializeField] private bool usePlayerMapCode;
    [SerializeField] private int[] needPlayerMapCode = new int[0];

    [SerializeField] private bool usePreEvents;
    [SerializeField] private int[] needPreGlobalEvents = new int[0];

#if UNITY_EDITOR
    [ArrayElementTitle("Title")]
#endif
    [SerializeField][SerializeReference] public List<GameEventAction> EventActions = new List<GameEventAction>();

    public void Update()
    {
        if (checkNeeds())
        {
            Debug.Log("Update GameEvent");
            if (EventActions.Count == 0) return;
            else
            {
                if (TickbasePlaySystem.instance.CurrentPlayingList.Count == 0 || (!TickbasePlaySystem.instance.CurrentPlayingList.Contains(this) && TickbasePlaySystem.instance.isPlayingEvent()))
                {
                    TickbasePlaySystem.instance.AddPlayingEvent(this);

                }
                else
                {
                    TickbasePlaySystem.instance.AddWaitingEvent(this);
                }
            }
        }
    }

    private bool checkNeeds()
    {
        if (useCharacterFeelings)
        {
            for (int i = 0; i < SystemSetting.count_MainActors; i++)
            {
                if (needCharacterFeelings[i] > PlayerManager.instance.autoSave.CharacterFeelings[i])
                    return false;
            }
        }
        if(useStats)
        {
            for (int i = 0; i < SystemSetting.count_Stats; i++)
            {
                if (needStats[i] > PlayerManager.instance.autoSave.PlayerStats[i])
                {
                    return false;
                }
            }
        }
        if (usePlayerMapCode)
        {
            for (int i = 0; i < needPlayerMapCode.Length; i++)
            {
                if (needPlayerMapCode[i] == PlayerManager.instance.autoSave.MapID)
                    return true;
            }
            return false;
        }
            return true;
    }

    public void initializeGameEvents()
    {
        for (int i = 0; i < EventActions.Count; i++)
        {
            EventActions[i].actionEnd = false;
        }
    }
    public void EventAllStop()
    {
        for (int i = 0; i < EventActions.Count; i++)
        {
            EventActions[i].actionEnd = true;
        }
    }
    public bool isGameEventEnd()
    {
        PlayerManager.instance.controller.isControl = true;
        TickbasePlaySystem.instance.EndEvent(this);
        return true;
    }
}
