using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndIf_Action : GameEventAction
{
    public EndIf_Action()
    {
        IFType = ifType.EndIF;
        Title = "EndIF";
    }
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        throw new System.NotImplementedException();
    }
}
