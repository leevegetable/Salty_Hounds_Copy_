using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_MapMove : GameEventAction
{
    public Action_MapMove()
    {
        ActionType = actionType.MapMove;
        Title = ActionType.ToString();
    }

    [SerializeField] private int mapCode;
    [SerializeField] private Vector2 mapPos;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        yield break;
    }
}
