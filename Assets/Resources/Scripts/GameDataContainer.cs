using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataContainer : MonoBehaviour
{
    public static GameDataContainer Instance;

    public SaveData[] SaveDataList = new SaveData[SystemSetting.capacity_SaveData];

    //public CharacterData[] CharacterDataList = new CharacterData[6];


    public EventData[] eventData;
    public ChoicesData[] choicesData = new ChoicesData[1];
    public NoticeData[] noticeData;

    private void Awake()
    {
        Instance = this;
    }
}
