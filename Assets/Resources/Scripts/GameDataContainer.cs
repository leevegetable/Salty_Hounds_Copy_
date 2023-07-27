using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataContainer : MonoBehaviour
{
    public static GameDataContainer Instance;

    public SaveData[] SaveDataList = new SaveData[SystemSetting.capacity_SaveData];
    
    public Dictionary<int, DialogueData> globalEventDialogueDatas = new Dictionary<int, DialogueData>();
    public Dictionary<int, DialogueData> characterEventDialogueDatas = new Dictionary<int, DialogueData>();
    public Dictionary<int, DialogueData> characterTalkDialogueDatas = new Dictionary<int, DialogueData>();
    public Dictionary<int, DialogueData> shopDialogueDatas = new Dictionary<int, DialogueData>();

    public Dictionary<int, NoticeData> PopUpDatas = new Dictionary<int, NoticeData>();

    public Dictionary<int, DialogueData> DialogueData(DialogueManager.DialoguePath Subpath)
    {
        switch (Subpath)
        {
            case DialogueManager.DialoguePath.GlobalEvent:
                return globalEventDialogueDatas;
            case DialogueManager.DialoguePath.CharacterEvent:
                return characterEventDialogueDatas;
            case DialogueManager.DialoguePath.CharacterTalk:
                return characterTalkDialogueDatas;
            case DialogueManager.DialoguePath.Shop:
                return shopDialogueDatas;
        }
        return null;
    }

    private void Awake()
    {
        Instance = this;
    }
}
