using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameEventAction
{
    public enum actionType { PopupMessage, Waiting, SpawnCharacter, DeleteCharacter, Move, MapMove, Dialogue, Calling }
    public actionType ActionType = actionType.Waiting;
    public bool isFold = true;
    [SerializeField] private bool isOverapNextAction = false;
    [SerializeField] private bool isPlayerControl = false;
    [SerializeField] private bool actionEnd = false;
    [SerializeField] public GameEventAction testCode;

    #region Universal Option
    [SerializeField] protected int targetID = 0;
    #endregion

    #region Waiting Option
    [SerializeField] private int h = 0;
    [SerializeField] private int m = 0;
    #endregion

    #region Spawn Option
    [SerializeField] private Vector2 spawnPos;
    #endregion

    #region Move Option
    [SerializeField] public enum target { Player, Character }
    [SerializeField] public target TargetType = target.Player;
    [SerializeField] private bool startPosCurrent;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    #endregion

    #region Map Move
    [SerializeField] private int mapCode;
    [SerializeField] private Vector2 mapPos;
    #endregion

    #region Dialogue Option
    public DialogueManager.Type DialogueType = DialogueManager.Type.GlobalEvent;
    [SerializeField] private int DialogueID;
    #endregion

    #region Calling Option
    public enum callingType { GlobalEvent, CharacterEvent, CharacterTalk }
    public callingType CallingType = callingType.GlobalEvent;
    [SerializeField] private int CallingID;
    #endregion

    public void update(GameEvent gameEvent)
    { }
    public void execute(GameEvent gameEvent)
    { }
}
