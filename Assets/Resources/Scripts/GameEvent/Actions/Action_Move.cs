using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_CharacterMove : GameEventAction
{
    public Action_CharacterMove()
    {
        ActionType = actionType.Move;
        Title = ActionType.ToString();
    }

    [SerializeField] public enum target { Player, Character }
    [SerializeField] public target TargetType = target.Player;
    [SerializeField] private int targetID = 0;
    [SerializeField] private bool startPosCurrent;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        gameEvent.isGameEventEnd();
        yield break;
    }
}
