using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpEditor : MonoBehaviour
{
    public NoticeData popUpData;
    public TMP_InputField InputID;
    public TMP_InputField MessageInputField;
    public Button Btn_LoadOrCreate;
    public TMP_Text Btn_LoadOrCreate_Text;
    public Button Btn_Apply;
    public string path = "";
    public bool isSearch;
    public void SearchStart()
    {
        MessageInputField.text = "";
        Btn_LoadOrCreate_Text.text = "Searching...";
        MessageInputField.interactable = false;
        Btn_LoadOrCreate.interactable = false;
        Btn_Apply.interactable = false;
    }
    public void SearchEnd()
    {
        path = IOManager.Instance.getPathIncludeFileExtansion(SystemSetting.path_NoticeMessageData, InputID.text, ".json");
        if (IOManager.Instance.isFileExist(path))
        {
            Btn_LoadOrCreate_Text.text = "Load File";
            Btn_LoadOrCreate.interactable = true;
            isSearch = true;
        }
        else
        {
            Btn_LoadOrCreate_Text.text = "Create File";
            Btn_LoadOrCreate.interactable = true;
            isSearch = false;
        }
    }

    public void LoadOrCreate()
    {
        if (isSearch)
        {
            popUpData = GameDataContainer.Instance.PopUpDatas[int.Parse(InputID.text)];
            MessageInputField.text = popUpData.Messages;
        }
        else
        {
            popUpData = new NoticeData();
            MessageInputField.text = "";
        }

        MessageInputField.interactable = true;
        Btn_LoadOrCreate.interactable = false;
        Btn_Apply.interactable = true;
    }

    public void Apply()
    {
        popUpData.ID = int.Parse(InputID.text);
        popUpData.Messages = MessageInputField.text;
        IOManager.Instance.WriteData(path, popUpData);
        IOManager.Instance.readPopUpMessage();
    }

}
