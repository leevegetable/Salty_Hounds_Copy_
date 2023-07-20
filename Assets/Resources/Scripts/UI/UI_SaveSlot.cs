using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_SaveSlot : MonoBehaviour
{
    public TMP_Text SaveTime;
    StringBuilder stringBuilder = new StringBuilder();

    public void init(SaveData data)
    {
        stringBuilder.Append(data.Week.ToString());
        stringBuilder.Append("аж / ");
        stringBuilder.Append(StringUtil.getDays(data.Day));
        stringBuilder.Append(" / ");
        stringBuilder.Append(data.Hour.ToString());
        if (data.Minute < 10)
        {
            stringBuilder.Append(":0");
            stringBuilder.Append(data.Minute);
        }
        else
        {
            stringBuilder.Append(':');
            stringBuilder.Append(data.Minute);
        }

        SaveTime.text = stringBuilder.ToString();
    }
}
