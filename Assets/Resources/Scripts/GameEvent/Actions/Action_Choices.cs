using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Choices : GameEventAction
{
    [ArrayElementTitle("OptionDescription")]
    [SerializeField]
    public Choice[] Choices = new Choice[0];

    public Action_Choices()
    {
        ActionType = actionType.Choices;
        Title = ActionType.ToString();
    }

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        actionEnd = false;
        PlayerManager.instance.controller.isControl = isPlayerControl;
        ChoiceManager.instance.setChoiceObjects(Choices,gameEvent,this);
        yield break;
    }
}
