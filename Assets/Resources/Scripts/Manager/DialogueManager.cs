using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public enum DialoguePath { GlobalEvent, CharacterEvent, CharacterTalk, Shop } //경로 설정.

    public DialoguePath DialogueType = DialoguePath.GlobalEvent;

    public ChoiceManager selectOptionManager;

    public GameEvent ref_CurrentgameEvent;
    public Action_Dialogue ref_GameEventAction;

    private Dictionary<int, DialogueData> CurPath;

    public GameObject DialoguePanel;
    public TMP_Text CharName;
    public TMP_Text Dialogue;
    private const float maxCount = 27;
    private float currentCount = 0;
    public int currentDialogueDataID = -1;
    public int currentDialogID = -1;
    private int maxLine;
    public int currentLine = -1;
    private bool isTyping = false;
    private string curLine;
    private string[] curEnterSplit;
    private string[] curSplit;
    public bool isOn = false;


    private void OnDisable()
    {
        init();
    }

    public void Start()
    {
        Instance = this;
    }

    public void CallDialogueData(GameEvent gameEvent, Action_Dialogue action, DialoguePath dialogueType, int ID)
    {
        DialoguePanel.SetActive(true);
        init(gameEvent, action, dialogueType, ID);

    }
    public void ChangeDialogueData(int ID)
    {
        DialoguePanel.SetActive(true);
        init(ID);
    }
    public void ChangeDialog(int ID)
    {
        currentDialogID = ID;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = CurPath[currentDialogueDataID].Dialogues[currentDialogID].Line.Length;

        DialoguePanel.SetActive(true);
        //UIManager.instance.calledDialogue();
    }

    public void DialogueDataEnd()
    {
        if (ref_GameEventAction != null)
            ref_GameEventAction.actionEnd = true;
        else
        {
            if(PlayerManager.instance != null && PlayerManager.instance.controller != null)
                PlayerManager.instance.controller.isControl = true;
        }

        init();
        isOn = false;
        DialoguePanel.SetActive(false);

    }

    private void init()
    {
        DialogueType = DialoguePath.GlobalEvent;
        currentDialogueDataID = -1;
        currentDialogID = -1;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = 0;
        CurPath = null;
    }

    private void init(int ID)
    {
        isOn = true;
        currentDialogueDataID = ID;
        currentDialogID = 0;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = CurPath[currentDialogueDataID].Dialogues[currentDialogID].Line.Length;
        Execute();
    }

    public void init(GameEvent gameEvent, Action_Dialogue action, DialoguePath dialogueType, int ID)
    {
        switch (dialogueType)
        {
            case DialoguePath.GlobalEvent:
                CurPath = GameDataContainer.Instance.globalEventDialogueDatas;
                break;
            case DialoguePath.CharacterEvent:
                CurPath = GameDataContainer.Instance.characterEventDialogueDatas;
                break;
            case DialoguePath.CharacterTalk:
                CurPath = GameDataContainer.Instance.characterTalkDialogueDatas;
                break;
            case DialoguePath.Shop:
                CurPath = GameDataContainer.Instance.shopDialogueDatas;
                break;
        }
        ref_CurrentgameEvent = gameEvent;
        ref_GameEventAction = action;
        DialogueType = dialogueType;
        currentDialogueDataID = ID;
        currentDialogID = 0;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = CurPath[currentDialogueDataID].Dialogues[currentDialogID].Line.Length;
        Execute();
    }

    public void NextLine()
    {
        if (currentDialogueDataID == -1) return;
        StopAllCoroutines();

        if (isTyping)
        {
            dialogSkip();
        }
        else
        {

            if (currentLine + 1 == maxLine)
            {
                if (currentDialogID + 1 == CurPath[currentDialogueDataID].Dialogues.Length)
                {
                    DialogueDataEnd();
                    return;
                }
                else
                {
                    currentDialogID++;
                    currentLine = 0;
                }
            }
            else
            {
                currentLine++;
            }
            if (currentDialogID < CurPath[currentDialogueDataID].Dialogues.Length)
            {

                maxLine = CurPath[currentDialogueDataID].Dialogues[currentDialogID].Line.Length;
                Execute();
            }
        }
    }

    public void Execute()
    {
        isTyping = true;
        ChoiceManager.instance.SelectEnd();
        StartCoroutine(execute(currentDialogueDataID));

    }

    private void CallSelectOptionGroup()
    {
        selectOptionManager.setChoiceObjects(ref_GameEventAction.hasChoices,ref_CurrentgameEvent,(GameEventAction)ref_GameEventAction);
        return;
    }

    public bool isLastDialogue()
    {
        if (currentDialogueDataID == -1) return true;
        if (currentDialogID + 1 >= CurPath[currentDialogueDataID].Dialogues.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void dialogSkip()
    {
        Dialogue.text = "";
        currentCount = 0;
        for (int i = 0; i < curSplit.Length; i++)
        {
            if (currentCount + curSplit[i].Length >= maxCount)
            {
                currentCount = 0;
                if (!curSplit[i].Contains('\n'))
                {
                    Dialogue.text += '\n';
                }
            }
            if (curSplit[i].Contains('\n'))
            {
                curEnterSplit = curSplit[i].Split('\n');
                Dialogue.text += curEnterSplit[0];
                Dialogue.text += '\n';
                Dialogue.text += curEnterSplit[1];
                currentCount = curEnterSplit[1].Length;
                curEnterSplit = null;
            }
            else
            {
                Dialogue.text += curSplit[i];
                currentCount += curSplit[i].Length;
            }
            if (i + 1 < curSplit.Length)
            {
                Dialogue.text += " ";
            }
        }
        if (currentLine + 1 == maxLine)
        {
            if (ref_GameEventAction != null && ref_GameEventAction.hasChoices != null)
            {
                CallSelectOptionGroup();
            }
        }

        isTyping = false;
    }
    private IEnumerator execute(int eventID)
    {
        Dialogue.text = "";
        currentCount = 0;
        curLine = CurPath[eventID].Dialogues[currentDialogID].Line[currentLine];
        curSplit = curLine.Split(' ');
        for (int i = 0; i < curSplit.Length; i++)
        {
            if (currentCount + curSplit[i].Length >= maxCount)
            {
                currentCount = 0;
                if (!curSplit[i].Contains('\n'))
                {
                    Dialogue.text += '\n';
                }
            }
            for (int j = 0; j < curSplit[i].Length; j++)
            {

                yield return new WaitForSeconds(0.1f);

                Dialogue.text += curSplit[i][j];
                if (curSplit[i][j] == '\n')
                    currentCount = 0;
                else
                    currentCount++;
            }
            if(i + 1 < curSplit.Length)
            {
                Dialogue.text += " ";
            }
        }
        if (currentLine + 1 == CurPath[currentDialogueDataID].Dialogues.Length)
        {
            //if (CurPath[currentDialogueDataID].Dialogues[currentDialogID].SelectOptionID != -1)
            //{
            //    CallSelectOptionGroup();
            //}
        }
        isTyping = false;
    }
}
