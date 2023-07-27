using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class DialogueDataEditor : MonoBehaviour
{

    public DialogueManager.DialoguePath SubPath;
    public DialogueData data;

    [SerializeField]
    private TMP_InputField DialogueID;
    [SerializeField]
    private TMP_Dropdown SubPathDropdown;
    [SerializeField]
    private Button Btn_LoadOrCreate;
    [SerializeField]
    private TMP_Text Button_LoadOrCreate;
    [SerializeField]
    private TMP_Text CurrentState;
    [SerializeField]
    private DialogList dialogList;
    [SerializeField]
    private Button Btn_Apply;
    public DialogOption option;


    private bool hasFile = false;

    private int targetID = 0;

    public DialogueData CurrentData;
    public int Current_Dialogue_Line_Index;
    public string Current_Dialogue_Line_Text;

    string path = null;

    private void Awake()
    {
        SubPathDropdown.AddOptions(Enum.GetNames(typeof(DialogueManager.DialoguePath)).ToList());
    }

    public void LoadOrCreateData()
    {
        if (hasFile)
        {
            switch (SubPath)
            {
                case DialogueManager.DialoguePath.GlobalEvent:
                    CurrentData = GameDataContainer.Instance.globalEventDialogueDatas[targetID];
                    break;
                case DialogueManager.DialoguePath.CharacterEvent:
                    CurrentData = GameDataContainer.Instance.characterEventDialogueDatas[targetID];
                    break;
                case DialogueManager.DialoguePath.CharacterTalk:
                    CurrentData = GameDataContainer.Instance.characterTalkDialogueDatas[targetID];
                    break;
                case DialogueManager.DialoguePath.Shop:
                    CurrentData = GameDataContainer.Instance.shopDialogueDatas[targetID];
                    break;
            }
            if (CurrentData.Dialogues != null)
                dialogList.ArrayDialog = CurrentData.Dialogues;
            else
                dialogList.ArrayDialog = null;
            dialogList.ListUpdate();
        }
        else
        {
            CurrentData = new DialogueData(targetID, null);
            dialogList.ArrayDialog = null;
            dialogList.ListUpdate();
        }
        Btn_Apply.interactable = true;
    }

    public void CreateDialog()
    {
        Dialog[] temp;
        if (dialogList.ArrayDialog == null)
        {
            temp = new Dialog[1];
        }
        else
        {
            temp = new Dialog[dialogList.ArrayDialog.Length + 1];
            for (int i = 0; i < dialogList.ArrayDialog.Length; i++)
            {
                temp[i] = dialogList.ArrayDialog[i];
            }

        }
        temp[temp.Length - 1] = new Dialog();
        dialogList.ArrayDialog = temp;
        dialogList.ListUpdate();
        dialogList.setCurrentSelectDialog(dialogList.ArrayDialog.Length - 1);
        
    }

    public void SaveDialog()
    {
        CurrentData.Dialogues = dialogList.ArrayDialog;
    }

    public void InputID()
    {
        Btn_LoadOrCreate.interactable = false;
        CurrentData = null;
        dialogList.ListUpdate();
        Btn_Apply.interactable = false;
        Button_LoadOrCreate.text = "Searching...";
        CurrentState.text = "Searching File...";
        option.init(null);

    }

    public void SetDropDown()
    {
        SubPath = (DialogueManager.DialoguePath)SubPathDropdown.value;
    }

    public void SearchID()
    {
        if (DialogueID.text != "")
            targetID = int.Parse(DialogueID.text);
        else
            targetID = 0;
        path = IOManager.Instance.getPathIncludeFileExtansion(SystemSetting.Write_Path_DialogueData, SubPathDropdown.options[SubPathDropdown.value].text + "/", DialogueID.text, ".json");
        if (IOManager.Instance.isFileExist(path))
        {
            hasFile = true;
            Button_LoadOrCreate.text = "Load File";
            CurrentState.text = "File retrieval successful";
            Button_LoadOrCreate.transform.parent.GetComponent<Button>().interactable = true;
        }
        else
        {
            hasFile = false;
            Button_LoadOrCreate.text = "Create File";
            CurrentState.text = "Failed to retrieve file";
            Button_LoadOrCreate.transform.parent.GetComponent<Button>().interactable = true;
        }
    }

    public void Apply()
    {
        path = IOManager.Instance.getPathIncludeFileExtansion(SystemSetting.Write_Path_DialogueData, SubPathDropdown.options[SubPathDropdown.value].text + "/", DialogueID.text, ".json");
        IOManager.Instance.WriteData(path, CurrentData);
        IOManager.Instance.readDialogue();
    }
}
#endif
