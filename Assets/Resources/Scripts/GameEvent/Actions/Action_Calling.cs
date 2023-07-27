using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Calling:GameEventAction
{
    //추후 커스텀 프로퍼티로 구분ㅇ ㅖ정
    public Action_Calling()
    {
        ActionType = actionType.Calling;
        Title = ActionType.ToString();
    }

    public enum callingPath { GlobalEvent, CharacterEvent, CharacterTalk }
    public callingPath CallingPath = callingPath.GlobalEvent;
    [SerializeField] private int CallingID;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        base.actionEnd = false;
        while (!actionEnd)
        {
            Debug.Log("Enter Action_Calling");
            yield return null;
            Debug.Log("Pop! Calling");
            actionEnd = true;
        }
        gameEvent.isGameEventEnd();
        Debug.Log("End Action_Calling");
        yield break;
    }
}
