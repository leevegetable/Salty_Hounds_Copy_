using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventExecutor : MonoBehaviour
{
    public static GameEventExecutor instance;
    public List<GameEvent> CurrentPlayingEvent = new List<GameEvent>();
    public void Awake()
    {
        instance = this;
    }

    public void Excute(GameEvent gameEvent)
    {
        if(!CurrentPlayingEvent.Contains(gameEvent))
        {
            CurrentPlayingEvent.Add(gameEvent);
            gameEvent.initializeGameEvents();
            Debug.Log(gameEvent.isGameEventEnd());
            StartCoroutine(gameEventExcute(gameEvent));
        }

    }
    IEnumerator gameEventExcute(GameEvent gameEvent)
    {
        int index = 0;
        GameEventAction currentAction = gameEvent.EventActions[index];
        List<GameEventAction> playingList = new List<GameEventAction>();
        List<int> inIF = new List<int>();
        Dictionary<int,int> SelectIF = new Dictionary<int,int>();
        UI_RuntimeLog.instance.CallLog(gameEvent.EventActions[index].ActionType.ToString());
        while (true)
        {
            if (currentAction.IFType != GameEventAction.ifType.none)
            {
                if (currentAction.IFType == GameEventAction.ifType.IF)
                {
                    inIF.Add(index);
                    if (currentAction.isIFTrue(gameEvent))
                    {
                        SelectIF.Add(inIF[inIF.Count - 1], index);
                        if (gameEvent.EventActions.IndexOf(currentAction) + 1 < gameEvent.EventActions.Count)
                        {
                            index = gameEvent.EventActions.IndexOf(currentAction) + 1;
                            currentAction = gameEvent.EventActions[index];
                        }
                        else
                        {
                            break;
                        }

                    }
                    else
                    {
                        index = currentAction.FalseIndex;
                        currentAction = gameEvent.EventActions[index];
                        continue;
                    }
                }
                else if (currentAction.IFType == GameEventAction.ifType.EndIF)
                {
                    if (SelectIF.ContainsKey(inIF[inIF.Count - 1]))
                        SelectIF.Remove(inIF[inIF.Count - 1]);
                    inIF.RemoveAt(inIF.Count - 1);
                    if (gameEvent.EventActions.IndexOf(currentAction) + 1 < gameEvent.EventActions.Count)
                    {
                        index = gameEvent.EventActions.IndexOf(currentAction) + 1;
                        currentAction = gameEvent.EventActions[index];
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (!SelectIF.ContainsKey(inIF[inIF.Count - 1]) && currentAction.isIFTrue(gameEvent))
                    {
                        SelectIF.Add(inIF[inIF.Count - 1], index);
                        {
                            index = gameEvent.EventActions.IndexOf(currentAction) + 1;
                            currentAction = gameEvent.EventActions[index];
                        }
                    }
                    else
                    {
                        index = currentAction.FalseIndex;
                        currentAction = gameEvent.EventActions[index];
                        continue;
                    }
                }
            }
            if (!playingList.Contains(currentAction))
            {
                StartCoroutine(currentAction.Execute(gameEvent));
                Debug.Log(currentAction.Title + " Execute");
                playingList.Add(gameEvent.EventActions[index]);
            }
            if (currentAction.actionEnd || currentAction.isOverapNextAction)
            {
                Debug.Log(currentAction.Title + " " + gameEvent.EventActions.Count + " " + playingList.Count);
                if (playingList.Count < gameEvent.EventActions.Count)
                {
                    index++;
                    currentAction = gameEvent.EventActions[index];
                    Debug.Log(currentAction.Title);
                }
            }
            if (gameEvent.EventActions.IndexOf(currentAction) + 1 >= gameEvent.EventActions.Count)
            {
                if (playingList.Count != 0)
                {
                    for (int i = 0; i < playingList.Count; i++)
                    {
                        yield return null;
                        if (!playingList[i].actionEnd)
                        {
                            i = 0;
                        }
                    }
                }
            }
            yield return null;
        }

        Debug.Log("isEnd");
        playingList.Clear();
        gameEvent.initializeGameEvents();
        gameEvent.isGameEventEnd();
        CurrentPlayingEvent.Remove(gameEvent);
    }
}
