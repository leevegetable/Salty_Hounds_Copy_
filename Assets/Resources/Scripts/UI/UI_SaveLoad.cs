using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SaveLoad : MonoBehaviour
{
    public UI_SaveSlot[] Slot = new UI_SaveSlot[6];
    private void OnEnable()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (GameDataContainer.Instance.SaveDataList.Length == 0) return;
        for(int i = 0; i < Slot.Length; i++)
        {
            if (GameDataContainer.Instance.SaveDataList[i].FileIndex == -1) continue;
            Slot[i].init(GameDataContainer.Instance.SaveDataList[i]);
        }
    }
}
