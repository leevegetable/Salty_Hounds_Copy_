using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Unity.Collections;

public class HourSchedule : ScriptableObject
{
    [Range(0,23)]public int StartHour;
    [Range(0,55)] public int StartMinute;
    [Range(0,23)]public int EndHour;
    [Range(0,55)] public int EndMinute;

    public GameEvent[] Events = new GameEvent[0];

    public void update(int id)
    {
        if (Events.Length == 0) return;
        else
        {

        }
    }
}
