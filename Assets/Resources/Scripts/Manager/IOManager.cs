using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;

public class IOManager:MonoBehaviour
{
    public static IOManager Instance;
    string tempData;
    SaveData testData = new SaveData();

    private void Awake()
    {
        Instance = this;
        //WriteCharacterData();
        //WriteSelectOptionGroup();
        //WriteEventData();
        WriteSaveData(0, testData);
        init();
        Debug.Log(Application.dataPath);
    }

    public void WriteSaveData(int index, SaveData saveData)
    {
        string path;
        path = Path.Combine(SystemSetting.path_SaveData, index + ".json");
        string SaveData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, SaveData);
    }
    public void WriteCharacterData()
    {
        string path;
        string data;
        path = Path.Combine(Application.dataPath, "CharacterData.Json");
        data = File.ReadAllText(path);
        //GameDataContainer.Instance.CharacterDataList[0] = JsonUtility.FromJson<UnityEditor.U2D.Animation.CharacterData>(data);
    }
    public void WriteChoicesData()
    {
        string path;
        string data;
        path = Path.Combine(SystemSetting.path_ChoicesData, "10000.Json");
        //data = JsonUtility.ToJson(new ChoicesData(0,false, new Choice[4] {
        //    new Choice(0, "�ȳ��ϼ���", null, new int[1], -1),
        //    new Choice(1,"�ȳ����輼��",new Choice.Reward[1],new int[1],-1),
        //    new Choice(2,"�ݰ�����",new Choice.Reward[1],new int[1],-1),
        //    new Choice(0,"�ȹݰ�����",new Choice.Reward[2]{Choice.Reward.statGrit, Choice.Reward.gold },new int[2]{1000, -1000 },-1)
        //}));
        //File.WriteAllText(path, data);
    }
    public void WriteEventData()
    {
        string path;
        StringBuilder folderPath = new StringBuilder();
        StringBuilder fileName = new StringBuilder();
        folderPath.Append(Application.dataPath);
        folderPath.Append("/Resources/Dialogue/kr/");
        fileName.Append("Events_");
        fileName.Append(1);
        fileName.Append(".json");
        path = Path.Combine(folderPath.ToString(), fileName.ToString());
        EventData test = new EventData(0,100,new DialogueData[5]);
        for (int i = 0; i < test.Dialogues.Length; i++)
        {
            test.Dialogues[i] = new DialogueData(0, -1, new string[5], -1);
        }
        string testcode = JsonUtility.ToJson(test,true);
        File.WriteAllText(path, testcode);
    }

    private void init()
    {
        //readSavefile();
        //readEventDialogue();
        //readSelectOptionGroup();
    }

    private void readSavefile()
    {
        FileInfo fileInfo;
        for (int i = 0; i < GameDataContainer.Instance.SaveDataList.Length; i++)
        {
            fileInfo = new FileInfo(SystemSetting.path_SaveData);
            if (!fileInfo.Exists)
            {
                GameDataContainer.Instance.SaveDataList[i].FileIndex = -1;
                continue;
            }
            tempData = File.ReadAllText(SystemSetting.path_SaveData);
            if (tempData != null)
            {
                GameDataContainer.Instance.SaveDataList[i] = JsonUtility.FromJson<SaveData>(tempData);
            }
        }
    }

    private void readEventDialogue()
    {
        GameDataContainer.Instance.eventData = new EventData[1];
        string path;
        StringBuilder folderPath = new StringBuilder();
        StringBuilder fileName = new StringBuilder();
        folderPath.Append(Application.dataPath);
        folderPath.Append("/Resources/Dialogue/kr/");
        fileName.Append("Events_");
        fileName.Append(1);
        fileName.Append(".json");
        path = Path.Combine(folderPath.ToString(), fileName.ToString());
        FileInfo fileInfo;
        fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
            return;
        }
        tempData = File.ReadAllText(path);
        GameDataContainer.Instance.eventData[0] = JsonUtility.FromJson<EventData>(tempData);
    }

    private void readSelectOptionGroup()
    {
        tempData = File.ReadAllText(Path.Combine(Application.dataPath, "SelectOptions_.Json"));
        GameDataContainer.Instance.choicesData[0] = JsonUtility.FromJson<ChoicesData>(tempData);
    }

}
