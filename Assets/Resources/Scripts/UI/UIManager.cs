using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameOption;
    public GameObject dialogue;
    public UI_PlayState playState;
    public GameObject CoverInteraction;
    public UI_Fade fade;

    public UI_PopUpMessage PopUp;

    public List<UI_StackItem> stackItems = new List<UI_StackItem>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (ControlManager.instance.isEscape)
        {
            calledEscapeKey();
        }
    }

    public void callFade()
    {
        CoverInteraction.SetActive(true);
        fade.animController.setTrigger("FadeOut");
    }
    public void unCallFade()
    {
        CoverInteraction.SetActive(false);
        fade.animController.setTrigger("FadeOut");
    }
    public void calledDialogue()
    {
        dialogue.SetActive(true);
    }
    private void calledEscapeKey()
    {
        if (stackItems.Count == 0)
        {
            gameOption.SetActive(true);
        }
        else
        {
            if (stackItems[stackItems.Count - 1].BlockEscapeKey)
            {
                gameOption.SetActive(true);
            }
            else
            {
                stackItems[stackItems.Count - 1].gameObject.SetActive(false);
            }
        }
    }

    public void CallPopUp(GameEventAction action, string Messages)
    {
        PopUp.gameObject.SetActive(true);
        PopUp.SetMessage(Messages,0);
    }
    public void unCallPopUp()
    {
        PopUp.init(null);
        PopUp.gameObject.SetActive(false);
    }
}
