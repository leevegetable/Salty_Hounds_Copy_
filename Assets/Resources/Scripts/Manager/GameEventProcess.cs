using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventProcess : MonoBehaviour
{
    public static GameEventProcess instance;
    public List<GameEvent> GlobalDialogueQueue = new List<GameEvent>();
    public List<GameEvent> CallingQueue = new List<GameEvent>();
    //public List<GameEvent> GlobalDialogueQueue = new List<GameEvent>();

    public bool EventIsPlaying
    {
        get
        {
            if (UIManager.instance.stackItems.Count == 0) return true;
            else return false;
        }
    }

    public void Execute()
    {
        
    }
    public void Sort()
    {
        
    }

    IEnumerator PlayEvent()
    {
        while (GlobalDialogueQueue.Count != 0)
        {
            if (!EventIsPlaying)
            {
                
            }
            yield return null;
        }
    }
}
