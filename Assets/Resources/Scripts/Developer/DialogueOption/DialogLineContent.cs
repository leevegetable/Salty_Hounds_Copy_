using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class DialogLineContent : MonoBehaviour
{
    public DialogLineList LineList;
    public TMP_Text DialogLinePreview;
    public InputingDialog inputing;
    public Outline outLine;
    public string dialogLineText;

    public void init(string text)
    {
        DialogLinePreview.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            DialogLinePreview.text += text[i];
            if (i > 20)
            {
                break;
            }
        }
        dialogLineText = text;
    }
    public void Destroy()
    {
        LineList.Lines.Remove(this);
        Destroy(gameObject);
    }
    public void SelectThis()
    {
        LineList.CurrentSelect = LineList.Lines.IndexOf(this);
        LineList.SelectItem(LineList.CurrentSelect);
        inputing.init(dialogLineText);
        inputing.TypingText.interactable = true;
        LineList.Option.TextEditor.LineContent = this;
    }
}
#endif
