using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class If_Time : GameEventAction
{
    public If_Time(ifType iftype, ifAction ifaction)
    {
        IFType = iftype;
        IFAction = ifaction;
        Title = IFType.ToString() + " " + ifaction.ToString();
    }
    public enum ifStartTime { none,more_than,Greater_than }
    public ifStartTime MoreOrGreater = ifStartTime.none;
    public int Start_hour = 0;
    public int Start_minute = 0;
    public enum ifEndTime { none,less,under }
    public ifEndTime LessOrUnder = ifEndTime.none;
    public int End_hour = 0;
    public int End_minute = 0;
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        throw new System.NotImplementedException();
    }

    public override bool isIFTrue(GameEvent gameEvent)
    {
        if (isStartTrue())
        {
            if (isEndTrue())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool isStartTrue()
    {
        if (MoreOrGreater == ifStartTime.none)
        {
            return true;
        }
        else if (MoreOrGreater == ifStartTime.more_than)
        {
            if (TickbasePlaySystem.instance.CurrentTick >= TickbasePlaySystem.getTimeToTick(Start_hour, Start_minute))
                return true;
            else
                return false;
        }
        else if (MoreOrGreater == ifStartTime.Greater_than)
        {
            if (TickbasePlaySystem.instance.CurrentTick > TickbasePlaySystem.getTimeToTick(Start_hour, Start_minute))
                return true;
            else
                return false;
        }
        return false;
    }
    private bool isEndTrue()
    {
        if (LessOrUnder == ifEndTime.none)
        {
            return true;
        }
        else if (LessOrUnder == ifEndTime.less)
        {
            if (TickbasePlaySystem.instance.CurrentTick <= TickbasePlaySystem.getTimeToTick(Start_hour, Start_minute))
                return true;
            else
                return false;
        }
        else if (LessOrUnder == ifEndTime.under)
        {
            if (TickbasePlaySystem.instance.CurrentTick < TickbasePlaySystem.getTimeToTick(Start_hour, Start_minute))
                return true;
            else
                return false;
        }
        return false;
    }
}
