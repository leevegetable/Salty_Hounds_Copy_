using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterObject
{
    void Update()
    {
        if (isControl)
        {
            if (ControlManager.instance.isSelect || ControlManager.instance.getVerticalDir != 0)
            {
                if (interactiveTarget != null)
                    Interacting();
            }
            if (ControlManager.instance.getHorizontalDir != 0 && !checkObstacle())
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    MoveSpeed = RunSpeed;
                else
                    MoveSpeed = WalkSpeed;
            }
            else
                MoveSpeed = 0;
            Actions.CharacterMove(ControlManager.instance.getHorizontalDir, MoveSpeed);
        }
    }

    public void Interacting()
    {
         if (interactiveTarget.key == Interaction.keyType.select && ControlManager.instance.isSelect)
         {
             interactiveTarget.Interacting(gameObject);
         }
         else if(interactiveTarget.key == Interaction.keyType.DirectionKey && ControlManager.instance.getVerticalDir != 0)
         {
             interactiveTarget.inputValue = ControlManager.instance.getVerticalDir;
             interactiveTarget.Interacting(gameObject);
         }
    }
}
