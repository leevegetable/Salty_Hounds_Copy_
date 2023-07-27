using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PopUpMessage : MonoBehaviour
{
    public TMP_Text message;
    public GameEventAction ref_gameEventAction;
    public void SetMessage(string text, int option)
    {
        message.text = text;
        //option에 따른 ui 효과
    }

    public void init(GameEventAction gameEventAction)
    {
        if (ref_gameEventAction != null)
        {
            ref_gameEventAction.actionEnd = true;
        }
        ref_gameEventAction = gameEventAction;
        message.text = null;
    }
}
