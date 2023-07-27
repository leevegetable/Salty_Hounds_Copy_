using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChoiceContent : MonoBehaviour
{
    public Choice selectOption;
    public TMP_Text Description;
    public GameEvent ref_CurrentGameEvent;
    public GameEventAction ref_gameEventAction;
    public UI_Choice_Needs[] OptionNeeds = new UI_Choice_Needs[2];
    public Button Btn_This;

    private void OnDisable()
    {
        init();
    }

    public void init(Choice Option)
    {
        Debug.Log(Option.OptionDescription);
        if (Option == null) return;
        selectOption = Option;
        Description.text = Option.OptionDescription;
        if (checkNeeds())
        {
            Btn_This.interactable = true;
        }
        else
        {
            Btn_This.interactable = false;
        }
    }
    public void init(Choice Option,GameEvent gameEvent ,GameEventAction gameEventAction)
    {
        ref_CurrentGameEvent = gameEvent;
        ref_gameEventAction = gameEventAction;
        if (Option == null) return;
        selectOption = Option;
        Description.text = Option.OptionDescription;
        if (checkNeeds())
        {
            Btn_This.interactable = true;
        }
        else
        {
            Btn_This.interactable = false;
        }
    }

    private void init()
    {
        selectOption = null;
        Description.text = "";
    }
    public void Execute()
    {
        PlayerManager.instance.autoSave.Gold -= selectOption.NeedGold;
        if (DialogueManager.Instance.isLastDialogue())
        {
            DialogueManager.Instance.DialogueDataEnd();
        }
        else
        {
            DialogueManager.Instance.NextLine();
        }
        if (selectOption.gameEvent != null)
        {
            if (ref_CurrentGameEvent != null)
            {
                ref_CurrentGameEvent.EventAllStop();
            }
            GameEventExecutor.instance.Excute(selectOption.gameEvent);
        }
        if(ref_gameEventAction != null)
        {
            ref_gameEventAction.actionEnd = true;
        }
        ChoiceManager.instance.SelectEnd();
        return;
    }
    public bool checkNeeds()
    {
        if (!intArrayMinus(PlayerManager.instance.autoSave.CharacterFeelings, selectOption.NeedCharacterFeeling))
            return false;
        if (!intArrayMinus(PlayerManager.instance.autoSave.PlayerStats, selectOption.NeedStat))
            return false;
        if (PlayerManager.instance.autoSave.Gold < selectOption.NeedGold)
            return false;
        return true;
    }
    public bool intArrayMinus(int[] array_1, int[] array_2)
    {
        for (int i = 0; i < array_1.Length; i++)
        {
            if (array_1[i] - array_2[i] < 0)
            {
                return false;
            }
        }
        return true;
    }
}
