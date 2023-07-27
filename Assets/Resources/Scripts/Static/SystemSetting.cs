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
    private const int count_mainActors = 5;
    private const int count_stats = 5;

    private const string write_Path_saveData = "/Resources/GameData/SaveData/";
    private const string write_Path_noticeMessageData = "/Resources/GameData/Message/Notice/";
    private const string write_Path_dialogueData = "/Resources/GameData/Dialogue/";
    private const string write_Path_choicesData = "/Resources/GameData/Choices/";
    private const string write_Path_gameEvent = "/Resources/GameData/GameEvent/";

    private const string path_saveData = "GameData/SaveData/";

    private const string path_noticeMessageData = "GameData/Message/Notice/";

    private const string path_dialogueData = "GameData/Dialogue/";

    private const string path_choicesData = "GameData/Choices/";

    private const string path_gameEvent = "GameData/GameEvent/";


    //참조 찾기 용도.
    public static int capacity_SaveData { get { return capacity_saveData; } }
    public static int capacity_Choices { get { return capacity_choices; } }
    public static int count_MainActors { get { return count_mainActors; } }
    public static int count_Stats { get { return count_stats; } }
    public static string path_SaveData { get {  return path_saveData; } }
    public static string path_NoticeMessageData { get { return path_noticeMessageData;} }
    public static string path_DialogueData { get { return path_dialogueData; } }
    public static string Write_Path_SaveData { get { return write_Path_saveData; } }
    public static string Write_Path_NoticeMessageData { get { return write_Path_noticeMessageData; } }
    public static string Write_Path_DialogueData { get { return write_Path_dialogueData; } }
    public static string Write_Path_ChoicesData { get { return write_Path_choicesData; } }
    public static string Write_Path_GameEvent { get { return write_Path_gameEvent; } }


    //커스텀 에디터 용도
#if UNITY_EDITOR
    #region Editor
    private const string path_hourSchedule = "Assets/Resources/ScriptableObject/CharacterTimeSchedule/";
    public static string path_HourSchedule { get { return path_hourSchedule; } }
    #endregion
#endif
}
