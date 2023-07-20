using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject gameOption;
    public GameObject dialogue;

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
    
}
