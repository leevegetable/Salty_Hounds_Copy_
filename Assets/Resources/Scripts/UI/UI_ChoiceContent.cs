using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChoiceContent : MonoBehaviour
{
    public Choice selectOption;
    public TMP_Text Description;
    public UI_Choice_Needs[] OptionNeeds = new UI_Choice_Needs[2];

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
        //if (selectOption.Rewards.Length != 0 && selectOption.Rewards[0] != Choice.Reward.none)
        //{
        //    if (selectOption.Rewards.Length < 2)
        //    {
        //        OptionNeeds[0].init(Option);
        //        OptionNeeds[0].gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        OptionNeeds[1].init(Option);
        //        OptionNeeds[1].gameObject.SetActive(true);
        //    }
        //}
    }

    private void init()
    {
        selectOption = null;
        Description.text = "";
    }

}
