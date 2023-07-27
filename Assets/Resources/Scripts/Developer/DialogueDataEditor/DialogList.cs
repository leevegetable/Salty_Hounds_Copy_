using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
public class DialogList : MonoBehaviour
{
    public DialogueDataEditor dialogueEditor;
    public List<DialogContent> dialogContentList = new List<DialogContent>();
    public TMP_Text DialogCount;
    public GameObject prefab;
    public Transform rect;
    public int currentSelect = -1;

    public Dialog[] ArrayDialog = new Dialog[0];

    public void Sort()
    {
        int index = 0;
        if (ArrayDialog.Length > 0)
        {
            for(int i = 0; i < ArrayDialog.Length; i++)
            {
                if (ArrayDialog[i] == null)
                {
                    if (i + 1 < ArrayDialog.Length)
                    {
                        ArrayDialog[i] = ArrayDialog[i + 1];
                        ArrayDialog[i + 1] = null;
                    }
                    else
                    {
                        index = i;
                        break;
                    }
                }
            }
            Dialog[] newArray = new Dialog[index];
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = ArrayDialog[i];
            }
            ArrayDialog = null;
            ArrayDialog = newArray;
        }
        DialogCount.text = ArrayDialog.Length.ToString();
    }

    public void ListUpdate()
    {
        if (dialogContentList.Count != 0)
        {
            while (dialogContentList.Count > 0)
            {
                dialogContentList[0].RemoveThis();
            }
            DialogCount.text = "0";
        }
        if (dialogueEditor.CurrentData == null) return;
        if (ArrayDialog == null) return;
        for (int i = 0; i < ArrayDialog.Length; i++)
        {
            GameObject temp = Instantiate(prefab,rect);
            dialogContentList.Add(temp.GetComponent<DialogContent>());
            dialogContentList[dialogContentList.Count - 1].init(ArrayDialog[i]);
            dialogContentList[dialogContentList.Count - 1].DialogList = this;
        }
        DialogCount.text = ArrayDialog.Length.ToString();
    }

    public void setCurrentSelectDialog(int id)
    {
        if (id < 0)
        {
            if (currentSelect != -1 && currentSelect < dialogContentList.Count)
            {
                if(dialogContentList.Count > 0)
                    dialogContentList[currentSelect].outLine.enabled = false;
            }
            currentSelect = id;
        }
        else
        {
            if(currentSelect != -1 && currentSelect < dialogContentList.Count)
                dialogContentList[currentSelect].outLine.enabled = false;
            currentSelect = id;
            dialogContentList[currentSelect].outLine.enabled = true;
            Debug.Log(ArrayDialog[currentSelect]);
            dialogueEditor.option.init(ArrayDialog[currentSelect]);
        }
    }
}
#endif
