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
                Day.text = "월";
                break;
            case 1:
                Day.text = "화";
                break;
            case 2:
                Day.text = "수";
                break;
            case 3:
                Day.text = "목";
                break;
            case 4:
                Day.text = "금";
                break;
            case 5:
                Day.text = "토";
                break;
            case 6:
                Day.text = "일";
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
