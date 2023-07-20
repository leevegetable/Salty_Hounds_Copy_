using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemSetting
{
    public enum  WorkerName
    {    
        none, LeeJongho
    }
    private const int capacity_saveData = 6;
    private const int capacity_choices = 5;
    private const int count_mainActors = 10;
    private const int count_stats = 5;

    private const string path_saveData = "Assets/Resources/GameData/SaveData/";

    private const string path_noticeMessageData = "Assets/Resources/GameData/Message/Notice/";

    private const string path_globalEventDialogueData = "Assets/Resources/GameData/Dialogue/Events/Global/";

    private const string path_characterEventDialogueData = "Assets/Resources/GameData/Dialogue/Events/Character/";

    private const string path_choicesData = "Assets/Resources/GameData/Choices/";


    //참조 찾기 용도.
    public static int capacity_SaveData { get { return capacity_saveData; } }
    public static int capacity_Choices { get { return capacity_choices; } }
    public static int count_MainActors { get { return count_mainActors; } }
    public static int count_Stats { get { return count_stats; } }
    public static string path_SaveData { get {  return path_saveData; } }
    public static string path_NoticeMessageData { get { return path_noticeMessageData;} }
    public static string path_GlobalEventDialogueData { get { return path_globalEventDialogueData; } }
    public static string path_CharacterEventDialogueData { get { return path_characterEventDialogueData;} }
    public static string path_ChoicesData { get { return path_choicesData; } }


    //커스텀 에디터 용도
#if UNITY_EDITOR
    #region Editor
    private const string path_hourSchedule = "Assets/Resources/ScriptableObject/CharacterTimeSchedule/";
    public static string path_HourSchedule { get { return path_hourSchedule; } }
    #endregion
#endif
}
