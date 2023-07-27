using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Waiting : GameEventAction
{
    public Action_Waiting()
    {
        ActionType = actionType.Waiting;
        Title = ActionType.ToString();
    }

    [SerializeField] private bool AllowEventStack = false;
    [SerializeField] private int h = 0;
    [SerializeField] private int m = 0;

    public override IEnumerator Execute(GameEvent gameEvent)
    {
        setPlayerControl();
        int targetTick = TickbasePlaySystem.instance.CurrentTick + TickbasePlaySystem.getTimeToTick(h, m);
        int snapShotTick = TickbasePlaySystem.instance.CurrentTick;
        while (TickbasePlaySystem.instance.CurrentTick < snapShotTick + targetTick - 1)
        {

            yield return new WaitForSeconds(1);
            if (AllowEventStack)
            {
                TickbasePlaySystem.instance.NextTick();
            }
            else
            {
                TickbasePlaySystem.instance.SkipTick();
            }
            Debug.Log(TickbasePlaySystem.instance.CurrentTick);
        }
        actionEnd = true;
        gameEvent.isGameEventEnd();
        yield return new WaitForSeconds(1);
        TickbasePlaySystem.instance.NextTick();
        Debug.Log(TickbasePlaySystem.instance.CurrentTick);
        yield break;
    }
}
