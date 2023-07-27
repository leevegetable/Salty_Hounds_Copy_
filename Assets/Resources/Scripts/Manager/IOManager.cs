using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class IOManager:MonoBehaviour
{
    public static IOManager Instance;
    StringBuilder stringBuilder = new StringBuilder();
    private string DataPath;
    private void Awake()
    {
        Instance = this;
        //기기별 저장경로가 다르기에 Define 지시어를 사용하여 구분하여야함.
        DataPath = Application.dataPath;
        //WriteCharacterData();
        //WriteSelectOptionGroup();
        //WriteEventData();

    }

    private void Start()
    {
        init();
    }

    private void init()
    {
        //CheckAndCreateFolderDirectory();
        //WriteSaveData(0);
        //readSavefile();
        readDialogue();
        readPopUpMessage();
    }
    private void CheckAndCreateFolderDirectory()
    {
        string path;
        path = getPath(SystemSetting.path_SaveData);
        if (!isFolderExist(path))
        {
            Directory.CreateDirectory(path);
        }
        path = getPath(SystemSetting.path_NoticeMessageData);
        if (!isFolderExist(path))
        {
            Directory.CreateDirectory(path);
        }
        for (int i = 0; i < Enum.GetValues(typeof(DialogueManager.DialoguePath)).Length; i++)
        {
            path = getPath(SystemSetting.path_DialogueData, Enum.GetName(typeof(DialogueManager.DialoguePath),i));
            if (!isFolderExist(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }

    #region SaveData
    public void WriteSaveData(int index)
    {
        if (PlayerManager.instance.autoSave == null)
        {
            Debug.Log("Player Save Data is Null");
            return;
        }
        string path = getPathIncludeFileExtansion(SystemSetting.path_SaveData, index.ToString(), ".json");
        string SaveData = JsonUtility.ToJson(PlayerManager.instance.autoSave, true);
        File.WriteAllText(path, SaveData);
    }
    private void readSavefile()
    {
        FileInfo fileInfo;
        string path;
        string readText;
        for (int i = 0; i < SystemSetting.capacity_SaveData; i++)
        {
            path = getPathIncludeFileExtansion(SystemSetting.path_SaveData, i.ToString(), ".json");
            if (!isFileExist(path))
            {
                GameDataContainer.Instance.SaveDataList[i].FileIndex = -1;
                continue;
            }
            fileInfo = new FileInfo(path);
            readText = File.ReadAllText(path);
            if (readText != null)
            {
                GameDataContainer.Instance.SaveDataList[i] = JsonUtility.FromJson<SaveData>(readText);
            }
        }
    }

    #endregion

    public void WriteData(string path, DialogueData data)
    {
        string fileText = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, fileText);
    }

    public void WriteData(string path, NoticeData data)
    {
        string fileText = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, fileText);
    }

    public void readDialogue()
    {
        string SubPath;
        string Path;
        for (int i = 0; i < Enum.GetValues(typeof(DialogueManager.DialoguePath)).Length; i++)
        {
            SubPath = Enum.GetName(typeof(DialogueManager.DialoguePath), i) +"/";
            Path = getPath(SystemSetting.path_DialogueData, SubPath);
            var FileList = Resources.LoadAll(Path);
            for (int j = 0; j < FileList.Length; j++)
            {
                if (!GameDataContainer.Instance.DialogueData((DialogueManager.DialoguePath)i).ContainsKey(int.Parse(FileList[j].name)))
                {
                    GameDataContainer.Instance.DialogueData((DialogueManager.DialoguePath)i).Add(int.Parse(FileList[j].name), null);
                }
                GameDataContainer.Instance.DialogueData((DialogueManager.DialoguePath)i)[int.Parse(FileList[j].name)] = JsonUtility.FromJson<DialogueData>(FileList[j].ToString());
            }
        }


    }

    public void readPopUpMessage()
    {
        var FileList = Resources.LoadAll(SystemSetting.path_NoticeMessageData);
        for (int i = 0; i < FileList.Length; i++)
        {
            if (!GameDataContainer.Instance.PopUpDatas.ContainsKey(int.Parse(FileList[i].name)))
            {
                GameDataContainer.Instance.PopUpDatas.Add(int.Parse(FileList[i].name), null);
            }
            GameDataContainer.Instance.PopUpDatas[int.Parse(FileList[i].name)] = JsonUtility.FromJson<NoticeData>(FileList[i].ToString());
        }
    }

    private string[] getFileList(string path)
    {
        var resources = Resources.LoadAll<TextAsset>(path);
        string[] fileList = new string[resources.Length];
        for (int i = 0; i < fileList.Length; i++)
        {
            fileList[i] = resources[i].text;
        }
        return fileList;
    }

    public string getPathIncludeFileExtansion(string path, string fileName, string fileExtansion)
    {
        stringBuilder.Clear();
        stringBuilder.Append(DataPath);
        stringBuilder.Append(path);
        stringBuilder.Append(fileName);
        stringBuilder.Append(fileExtansion);
        return stringBuilder.ToString();
    }
    public string getPathIncludeFileExtansion(string path, string subPath, string fileName, string fileExtansion)
    {
        stringBuilder.Clear();
        stringBuilder.Append(DataPath);
        stringBuilder.Append(path);
        stringBuilder.Append(subPath);
        stringBuilder.Append(fileName);
        stringBuilder.Append(fileExtansion);
        return stringBuilder.ToString();
    }

    public string getPath(string path)
    {
        stringBuilder.Clear();
        stringBuilder.Append(DataPath);
        stringBuilder.Append(path);
        return stringBuilder.ToString();
    }

    public string getPath(string path, string subPath)
    {
        stringBuilder.Clear();
        stringBuilder.Append(path);
        stringBuilder.Append(subPath);
        return stringBuilder.ToString();
    }

    public bool isFileExist(string path)
    {
        return new FileInfo(path).Exists;
    }
    public bool isFolderExist(string path)
    {
        return new DirectoryInfo(path).Exists;
    }

}
