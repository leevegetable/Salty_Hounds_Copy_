using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class TestExecuteDialogue : MonoBehaviour
{
    DialogueManager.DialoguePath SubPath;
    public TMP_InputField inputID;
    public TMP_Dropdown dropDown;

    private void OnEnable()
    {
        dropDown.AddOptions(Enum.GetNames(typeof(DialogueManager.DialoguePath)).ToList());
    }

    public void Execute()
    {
        switch (dropDown.value)
        {
            case 0:
                SubPath = DialogueManager.DialoguePath.GlobalEvent;
                break;
            case 1:
                SubPath = DialogueManager.DialoguePath.CharacterEvent;
                break;
            case 2:
                SubPath = DialogueManager.DialoguePath.CharacterTalk;
                break;
            case 3:
                SubPath = DialogueManager.DialoguePath.Shop;
                break;
        }
        DialogueManager.Instance.CallDialogueData(null,null, SubPath,int.Parse(inputID.text));
    }

   
}
#endif