using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Fadeout : GameEventAction
{
    public Action_Fadeout()
    {
        ActionType = actionType.FadeOut;
        Title = ActionType.ToString();
    }
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        actionEnd = false;
        PlayerManager.instance.controller.isControl = isPlayerControl;
        UIManager.instance.callFade();
        while (!UIManager.instance.fade.animController.isEnd("UI_FadeIn"))
        {
            Debug.Log(UIManager.instance.fade.animController.animator.GetCurrentAnimatorClipInfo(0).ToString());
            yield return null;
        }
        PlayerManager.instance.controller.isControl = true;
        actionEnd = true;
        UIManager.instance.unCallFade();
        yield break;
    }
}
