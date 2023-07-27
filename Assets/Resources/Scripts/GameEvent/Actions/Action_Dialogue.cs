using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Dialogue : GameEventAction
{
    public DialogueManager.DialoguePath DialogueType = DialogueManager.DialoguePath.GlobalEvent;
    [SerializeField] private int DialogueID;
    public Choice[] hasChoices = new Choice[0];

    public Action_Dialogue()
    {
        ActionType = actionType.Dialogue;
        Title = ActionType.ToString();
    }

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        actionEnd = false;
        UI_RuntimeLog.instance.CallLog("Enter Action Coroutine");
        DialogueManager.Instance.CallDialogueData(gameEvent, this, DialogueType, DialogueID);
        yield return new WaitUntil(() => actionEnd);
        gameEvent.isGameEventEnd();
    }

}
