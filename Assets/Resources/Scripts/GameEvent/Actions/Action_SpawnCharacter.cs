using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_SpawnCharacter : GameEventAction
{
    public Action_SpawnCharacter()
    {
        ActionType = actionType.SpawnCharacter;
        Title = ActionType.ToString();
    }

    [SerializeField] private Vector2 spawnPos;
    [SerializeField] private int targetID = 0;
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        gameEvent.isGameEventEnd();
        throw new System.NotImplementedException();
    }
}
