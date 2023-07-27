using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class DialogContent : MonoBehaviour
{
    public DialogList DialogList;
    [SerializeField]
    private Dialog dialog;
    [SerializeField]
    private TMP_Text ActorID;
    [SerializeField]
    private TMP_Text DialogCount;
    [SerializeField]
    private CanvasGroup eventCgCanvasGroup;
    [SerializeField]
    private CanvasGroup choicesCanvasGroup;
    public Outline outLine;

    public void init(Dialog _dialog)
    {
        dialog = _dialog;
        if (dialog == null)
        {
            return;
        }
        ActorID.text = "Actor: " + dialog.NpcID;
        if (dialog.Line == null) return;
        DialogCount.text = "DialogCount: " + dialog.Line.Length;
    }

    public void SelectThis()
    {
        DialogList.setCurrentSelectDialog(DialogList.dialogContentList.IndexOf(this));
    }
    public void DeleteThis()
    {
        int index = DialogList.dialogContentList.IndexOf(this);
        Debug.Log("Delete!" + index);
        DialogList.ArrayDialog[index] = null;
        DialogList.Sort();
        RemoveThis();
        DialogList.setCurrentSelectDialog(DialogList.dialogContentList.Count - 1);
    }

    public void RemoveThis()
    {
        DialogList.dialogContentList.Remove(this);
        Destroy(gameObject);
    }

    private void SetChoices(int selecOptionID)
    {
        if (selecOptionID < 0) choicesCanvasGroup.alpha = 0.5f;
        else choicesCanvasGroup.alpha = 1f;
    }
    private void SetEventCg(int EventCgID)
    {
        if (EventCgID < 0) eventCgCanvasGroup.alpha = 0.5f;
        else eventCgCanvasGroup.alpha = 1f;
    }

}
#endif
