using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
public class InputingDialog : MonoBehaviour
{
    [SerializeField] public DialogLineContent LineContent;
    [SerializeField]
    public TMP_InputField TypingText;
    [SerializeField]
    private bool isEnterReady = false;
    string cur_Line;
    [SerializeField]
    string[] curEnterSplit = null;
    string[] curSplit = null;
    string temp = "";
    private const float maxCount = 27;

    public void init(string dialogLine)
    {
        TypingText.text = "";
        if (dialogLine == null) return;
        curEnterSplit = dialogLine.Split('\n');
        string[] arrayTemp = null;

        for (int i = 0; i < curEnterSplit.Length; i++)
        {
            Debug.Log(curEnterSplit[i]);
        }
        if (curEnterSplit != null)
        {
            arrayTemp = sorting(curEnterSplit);
        }

        else
            arrayTemp = new string[0];
        for(int i= 0; i < arrayTemp.Length; i++)
        {
            TypingText.text += arrayTemp[i];
            TypingText.text += '\n';
        }
        TypingText.MoveTextEnd(false);
    }

    private string[] sorting(string[] _arrayString)
    {
        string[] arrayString = _arrayString;
        for(int i = 0; i < arrayString.Length; i++)
        {
            if (arrayString[arrayString.Length - 1] == null)
            {
                arrayString[arrayString.Length - 1] = "";
            }
            if (arrayString[i].Length > maxCount)
            {
                if (i + 1 >= arrayString.Length)
                {
                    string[] arrayTemtemp = new string[arrayString.Length + 1];
                    for (int j = 0; j < arrayString.Length; j++)
                    {
                        arrayTemtemp[j] = arrayString[j];
                    }
                    arrayString = arrayTemtemp;
                }
                else
                {
                    string temp = arrayString[i + 1];
                    string[] arrayTemp = arrayString[i].Split(' ');
                    arrayString[i + 1] = arrayTemp[arrayTemp.Length - 1] + ' ';
                    arrayString[i + 1] += temp;
                    arrayString[i] = "";
                    for (int j = 0; j < arrayTemp.Length - 1; j++)
                    {
                        arrayString[i] += arrayTemp[j];
                        arrayString[i] += " ";
                    }
                }
            }
        }
        return arrayString;
    }

    public void StartEditDialogue()
    {
        if (!isEnterReady)
            StartCoroutine(split());
    }
    public void EndEditDialogue()
    {
        StopCoroutine(split());
        LineContent.init(TypingText.text);
        isEnterReady = false;
    }

    IEnumerator split()
    {
        isEnterReady = true;
        curEnterSplit = null;
        curSplit = null;
        while (true)
        {
            yield return null;
            cur_Line = TypingText.text;
            curEnterSplit = cur_Line.Split('\n');
            curSplit = curEnterSplit[curEnterSplit.Length - 1].Split(' ');
            temp = "";

            if (curEnterSplit[curEnterSplit.Length - 1].Length > maxCount)
            {
                curEnterSplit[curEnterSplit.Length - 1] = "";
                for (int i = 0; i < curSplit.Length; i++)
                {
                    if (temp.Length + curSplit[i].Length > maxCount)
                    {
                        curEnterSplit[curEnterSplit.Length - 1] = temp;
                        temp = "";
                        temp += curSplit[i];
                        break;
                    }
                    temp += curSplit[i];
                    temp += " ";
                }
                break;
            }
        }
        TypingText.text = null;
        for (int i = 0; i < curEnterSplit.Length; i++)
        {
            TypingText.text += curEnterSplit[i];
            TypingText.text += '\n';
        }
        TypingText.text += temp;
        TypingText.MoveTextEnd(false);
        isEnterReady = false;
    }

}
#endif