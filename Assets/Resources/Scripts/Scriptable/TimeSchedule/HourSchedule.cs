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

    public GameEvent gameEvent;

    public void update(int id)
    {
        Debug.Log("Update");
        if (gameEvent == null) return;
        else
        {
            gameEvent.Update();
        }
    }
}
