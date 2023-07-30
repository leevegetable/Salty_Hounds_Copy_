using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_DeleteCharacter : GameEventAction
{
    public Action_DeleteCharacter()
    {
        ActionType = actionType.DeleteCharacter;
        Title = ActionType.ToString();
    }
    [SerializeField] private int targetID = 0;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        throw new System.NotImplementedException();
    }
}
