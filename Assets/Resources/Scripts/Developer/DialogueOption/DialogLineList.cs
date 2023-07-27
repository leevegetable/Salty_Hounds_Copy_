using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class DialogLineList : MonoBehaviour
{
    public DialogOption Option;
    public string[] CurrentHasLine;
    public List<DialogLineContent> Lines = new List<DialogLineContent>();
    public int CurrentSelect = -1;
    public Transform ContentsField;
    public GameObject ContentsPrefab;

    public void CreateContents()
    {
        GameObject content = Instantiate(ContentsPrefab);
        content.transform.parent = ContentsField;
        Lines.Add(content.GetComponent<DialogLineContent>());
        Lines[Lines.Count - 1].LineList = this;
        Lines[Lines.Count - 1].inputing = Option.TextEditor.GetComponent<InputingDialog>();
    }

    public void init(string[] lines)
    {
        CurrentHasLine = lines;

        if (Lines.Count != 0)
        {
            while (Lines.Count > 0)
            {
                Lines[0].Destroy();
            }
        }
        if (lines == null) return;
        for (int i = 0; i < lines.Length; i++)
        {
            CreateContents();
            Lines[i].init(lines[i]);
        }
    }
    public void SelectItem(int index)
    {
        if (index < 0)
        {
            Option.TextEditor.gameObject.SetActive(false);
            if (CurrentSelect > -1)
            {
                Lines[CurrentSelect].outLine.enabled = false;
            }
            CurrentSelect = index;
            return;
        }
        if (CurrentSelect < 0)
        {
            CurrentSelect = index;
            return;
        }
        else
        {
            Lines[CurrentSelect].outLine.enabled = false;
            CurrentSelect = index;
        }
        Option.TextEditor.gameObject.SetActive(true);
        Lines[CurrentSelect].outLine.enabled = true;

    }
    public void returnData()
    {
        string[] CustomLines = new string[Lines.Count];
        for(int i = 0;i < CustomLines.Length;i++)
        {
            CustomLines[i] = Lines[i].dialogLineText;
        }
        CurrentHasLine = null;
        CurrentHasLine = CustomLines;
        Option.targetDialog.Line = null;
        Option.targetDialog.Line = CurrentHasLine;
        Debug.Log("Option.targetDialog.Line Count" + Option.targetDialog.Line.Length);
        SelectItem(-1);
    }
}
#endif