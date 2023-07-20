using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public enum Type { GlobalEvent, CharacterEvent, CharacterTalk, Shop } //경로 설정.

    public Type DialogueType = Type.GlobalEvent;

    public ChoiceManager selectOptionManager;

    public TMP_Text CharName;
    public TMP_Text Dialogue;
    private float maxCount = 27;
    private float currentCount = 0;
    private int currentID = -1;
    private int currentDialogueID = -1;
    private int maxLine;
    private int currentLine = -1;
    private bool isTyping = false;
    private string curLine;
    private string[] curSplit;

    private void OnDisable()
    {
        init();
    }

    public void CallDialogue(Type dialogueType, int ID)
    {
        init(dialogueType, ID);
        UIManager.instance.calledDialogue();
    }

    private void init()
    {
        DialogueType = Type.GlobalEvent;
        currentID = -1;
        currentDialogueID = -1;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = 0;
    }



    private void init(Type dialogueType, int ID)
    {
        DialogueType = dialogueType;
        currentID = ID;
        currentDialogueID = 0;
        currentLine = 0;
        currentCount = 0;
        Dialogue.text = "";
        maxLine = GameDataContainer.Instance.eventData[currentID].Dialogues[currentDialogueID].Line.Length;
    }

    public void NextLine()
    {
        if (currentID == -1) return;
        if (currentLine + 1 == maxLine && GameDataContainer.Instance.eventData[currentID].Dialogues[currentDialogueID].SelectOptionID != -1)
        {
            CallSelectOptionGroup();
            return;
        }

        StopAllCoroutines();
        Dialogue.text = "";
        currentCount = 0;

        if (isTyping)
        {
            for (int i = 0; i < curSplit.Length; i++)
            {
                if (currentCount + curSplit[i].Length >= maxCount)
                {
                    currentCount = 0;
                    Dialogue.text += '\n';
                }
                Dialogue.text += curSplit[i];
                currentCount += curSplit[i].Length;
                if (i + 1 < curSplit.Length)
                {
                    Dialogue.text += " ";
                }
            }
            isTyping = false;
        }
        else
        {
            currentLine++;
            if (currentLine >= maxLine)
            {
                currentDialogueID++;
                currentLine = 0;
            }
            if (currentDialogueID < GameDataContainer.Instance.eventData[currentID].Dialogues.Length)
            {
                maxLine = GameDataContainer.Instance.eventData[currentID].Dialogues[currentDialogueID].Line.Length;
                Execute();
            }
            else
            {
                return;
            }
        }
    }

    public void Execute()
    {
        isTyping = true;
        StartCoroutine(eventDialogue(currentID));

    }

    private void CallSelectOptionGroup()
    {
        selectOptionManager.setChoiceObjects(GameDataContainer.Instance.choicesData[0]);
        return;
    }

    private IEnumerator eventDialogue(int eventID)
    {
        curLine = GameDataContainer.Instance.eventData[eventID].Dialogues[currentDialogueID].Line[currentLine];
        curSplit = curLine.Split(' ');
        for (int i = 0; i < curSplit.Length; i++)
        {
            if (currentCount + curSplit[i].Length >= maxCount)
            {
                currentCount = 0;
                Dialogue.text += '\n';
            }
            for (int j = 0; j < curSplit[i].Length; j++)
            {

                yield return new WaitForSeconds(0.1f);
                Dialogue.text += curSplit[i][j];
                currentCount++;
            }
            if(i + 1 < curSplit.Length)
            {
                Dialogue.text += " ";
            }
        }
            isTyping = false;
    }
}
