using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Else_Action : GameEventAction
{
    public Else_Action(ifType iftype)
    {
        IFType = iftype;
        Title = IFType.ToString();
    }
    public override IEnumerator Execute(GameEvent gameEvent)
    {
        throw new System.NotImplementedException();
    }
    public override bool isIFTrue(GameEvent gameEvent)
    {
        return false;
    }
}
