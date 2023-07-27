using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
public class DialogOption : MonoBehaviour
{
    public Dialog targetDialog;
    public TMP_InputField npcID;
    public TMP_InputField npcFaceID;
    public TMP_InputField EventCGID;
    public DialogLineList LineList;
    public InputingDialog TextEditor;
    public Button Btn_Add;
    public Button Btn_Save;

    
    public void init(Dialog _dialog)
    {
        if (_dialog == null)
        {
            npcID.text = "";
            npcID.interactable = false;
            npcFaceID.text = "";
            npcFaceID.interactable = false;
            EventCGID.text = "";
            EventCGID.interactable = false;
            LineList.init(null);
        }
        else
        {
            Btn_Add.interactable = true;
            Btn_Save.interactable = true;
            npcID.interactable = true;
            npcFaceID.interactable = true;
            EventCGID.interactable = true;

            targetDialog = _dialog;
            npcID.text = targetDialog.NpcID.ToString();
            npcFaceID.text = targetDialog.NpcFace.ToString();
            EventCGID.text = targetDialog.EventCGID.ToString();
            LineList.init(targetDialog.Line);
        }
    }

    public void SaveData()
    {
        if (targetDialog == null) return;
        targetDialog.NpcID = int.Parse(npcID.text);
        targetDialog.NpcFace = int.Parse(npcFaceID.text);
        targetDialog.EventCGID = int.Parse(EventCGID.text);
        LineList.returnData();
    }
}
#endif