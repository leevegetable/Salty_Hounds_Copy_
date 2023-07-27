using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_PopupMessage : GameEventAction
{
    public Action_PopupMessage()
    {
        ActionType = actionType.PopupMessage;
        Title = ActionType.ToString();
    }
    [SerializeField] private int MessageID;
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        //UIManager.instance.CallPopUp(this,MessageID);
        //yield return new WaitUntil(() => actionEnd);
        actionEnd = true;
        gameEvent.isGameEventEnd();
        yield break;
    }
}
