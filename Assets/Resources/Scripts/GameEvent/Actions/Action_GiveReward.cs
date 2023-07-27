using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_GiveReward : GameEventAction
{
    public int[] stat = new int[5];
    public int[] feelings = new int[5];
    public int Gold = 0;
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        for (int i = 0; i < stat.Length; i++)
        {
            if (stat[i] != 0)
            {

                UIManager.instance.CallPopUp(this, "���� " + i + "�� " + stat[i] + "�ö��� !");
                PlayerManager.instance.autoSave.PlayerStats[i] += stat[i];
                UIManager.instance.playState.Refresh();
                yield return new WaitForSeconds(1);
            }
        }
        for (int i = 0; i < feelings.Length; i++)
        {
            if (feelings[i] != 0)
            {
                UIManager.instance.CallPopUp(this, i + "�� ȣ������" + feelings[i] + "�ö��� !");
                PlayerManager.instance.autoSave.CharacterFeelings[i] += stat[i];
                UIManager.instance.playState.Refresh();
                yield return new WaitForSeconds(1);
            }

        }
        if (Gold != 0)
        {
            UIManager.instance.CallPopUp(this, "�������� " + Gold + "�����Ͽ���!");
            PlayerManager.instance.autoSave.Gold += Gold;
            UIManager.instance.playState.Refresh();
            yield return new WaitForSeconds(1);
        }
        UIManager.instance.unCallPopUp();
        yield break;
    }
}
