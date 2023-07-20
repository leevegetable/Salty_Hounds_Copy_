using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] private bool useCharacterFeelings;
    [SerializeField] private int[] needCharacterFeelings = new int[SystemSetting.count_MainActors];
    [SerializeField] private bool useStats;
    [SerializeField] private int[] needStats = new int[SystemSetting.count_Stats];
    [SerializeField] private bool usePlayerMapCode;
    [SerializeField] private int[] needPlayerMapCode = new int[0];
    [SerializeField] private bool usePreEvents;
    [SerializeField] private int[] needPreEvents = new int[0];
    [SerializeField] private bool giveCharacterFeelings;
    [SerializeField] private int[] rewardCharacterFeelings = new int[SystemSetting.count_MainActors];
    [SerializeField] private bool giveStats;
    [SerializeField] private int[] rewardStats = new int[SystemSetting.count_Stats];
    [SerializeField] private bool giveGold;
    [SerializeField] private int rewardGold = 0;
    [SerializeField] public GameEventAction[] EventActions = new GameEventAction[0];

    public void Update()
    {
        if (checkNeeds())
        {
            if (EventActions.Length == 0) return;
            for(int i = 0; i < EventActions.Length; i++)
            {
                EventActions[i].update(this);
            }
        }
    }

    public void Execute()
    {
        
    }

    private bool checkNeeds()
    {

        if (useCharacterFeelings)
        {
            for (int i = 0; i < SystemSetting.count_MainActors; i++)
            {
                if (needCharacterFeelings[i] > PlayerManager.instanse.autoSave.CharacterFeelings[i])
                    return false;
            }
        }
        if(useStats)
        {
            for (int i = 0; i < SystemSetting.count_Stats; i++)
            {
                if (needStats[i] > PlayerManager.instanse.autoSave.PlayerStats[i])
                {
                    return false;
                }
            }
        }
        if (usePlayerMapCode)
        {
            for (int i = 0; i < needPlayerMapCode.Length; i++)
            {
                if (needPlayerMapCode[i] == PlayerManager.instanse.autoSave.MapID)
                    return true;
            }
            return false;
        }
        else
        {
            return true;
        }

    }

}
