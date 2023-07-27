using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class GameEventAction
{
    [HideInInspector]
    public string Title;
    public enum actionType { PopupMessage, Waiting, SpawnCharacter, DeleteCharacter, Move, MapMove, Dialogue, Calling, Choices, FadeOut, GiveReward }
    [HideInInspector]
    public actionType ActionType = actionType.Waiting;
    public enum ifType { none, IF, ElseIF, Else ,EndIF }
    //[HideInInspector]
    [HideInInspector]
    public ifType IFType = ifType.none;
    public enum ifAction { none, Time }
    [HideInInspector]
    public ifAction IFAction = ifAction.Time;
    [SerializeField] public bool isOverapNextAction = false;
    [SerializeField] public bool isPlayerControl = false;
    public bool actionEnd = false;
    [HideInInspector]
    public int FalseIndex;

    public void setPlayerControl()
    {
        PlayerManager.instance.controller.isControl = isPlayerControl;
    }
    public abstract IEnumerator Execute(GameEvent gameEvent);

    public virtual bool isIFTrue(GameEvent gameEvent)
    {
        return false;
    }

}
