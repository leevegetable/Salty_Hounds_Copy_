using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    [SerializeField]
    public string OptionDescription;
    public int[] NeedCharacterFeeling = new int[5];
    [SerializeField]
    public int[] NeedStat = new int[5];
    public int NeedGold = 0;
    public GameEvent gameEvent;
    public Choice() 
    {
        OptionDescription = null;
        NeedCharacterFeeling = new int[5];
        NeedStat = new int[5];
        NeedGold = 0;
    }
    public Choice(string optionDescription)
    {
        OptionDescription = optionDescription;
        NeedCharacterFeeling = new int[5];
        NeedStat = new int[5];
        NeedGold = 0;
    }
}
