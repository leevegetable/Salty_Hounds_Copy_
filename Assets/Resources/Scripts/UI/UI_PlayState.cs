using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayState : MonoBehaviour
{
    public TMP_Text HourAndMinute;
    public TMP_Text Week;
    public TMP_Text Day;

    public TMP_Text Energy;
    public TMP_Text[] Stat;

    public TMP_Text Gold;

    public void Refresh()
    {
        SetTime(TickbasePlaySystem.getTickToTime(TickbasePlaySystem.instance.CurrentTick),
            TickbasePlaySystem.instance.CurrentWeek, TickbasePlaySystem.instance.CurrentDay);
        SetStat(PlayerManager.instance.autoSave.PlayerStats);
        SetEnergy(PlayerManager.instance.autoSave.curEnergy, PlayerManager.instance.autoSave.maxEnergy);
        SetGold(PlayerManager.instance.autoSave.Gold);
    }

    public void SetTime(int[] arrayTime, int week, int day)
    {
        HourAndMinute.text = arrayTime[0].ToString() + " : " + arrayTime[1].ToString();
        Week.text = week.ToString();
        switch (day)
        {
            case 0:
                Day.text = "��";
                break;
            case 1:
                Day.text = "ȭ";
                break;
            case 2:
                Day.text = "��";
                break;
            case 3:
                Day.text = "��";
                break;
            case 4:
                Day.text = "��";
                break;
            case 5:
                Day.text = "��";
                break;
            case 6:
                Day.text = "��";
                break;
        }
    }

    public void SetEnergy(int curEnergy, int maxEnergy)
    {
        Energy.text = curEnergy.ToString() + " / " + maxEnergy.ToString();
    }

    public void SetStat(int[] arrayStat)
    {
        for(int i = 0; i < arrayStat.Length; i++)
        {
            Stat[i].text = arrayStat[i].ToString();
        }
    }

    public void SetGold(int gold)
    {
        Gold.text = gold.ToString();
    }
}
