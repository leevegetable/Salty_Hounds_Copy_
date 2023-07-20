using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public int ID;
    public string OptionDescription;
    private int[] RewardCharacterFeeling = new int[5];
    [SerializeField]
    private int[] RewardStat = new int[5];
    [SerializeField]
    private int RewardGold = 0;
    public int NextDialogue;
    public Choice() 
    {
        ID = -1;
        OptionDescription = null;
        NextDialogue = -1;
    }
    public Choice(int id, string optionDescription, int[] characterFeeling, int[] rewardValue, int nextDialogue)
    {
        ID = id;
        OptionDescription = optionDescription;
        RewardCharacterFeeling = characterFeeling;
        RewardStat = rewardValue;
        RewardGold = 0;
        NextDialogue = nextDialogue;
    }
}
