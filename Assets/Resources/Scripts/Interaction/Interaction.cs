using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Interaction : MonoBehaviour
{
    public enum Type {trigger, contact}
    public enum keyType { select, DirectionKey }
    public Type type = Type.trigger;
    public keyType key = keyType.select;
    public delegate void interact(GameObject target);
    public interact Interactions;
    public float inputValue = 0;
    public GameEvent gameEvent;

    public void Interacting(GameObject target)
    {
        Debug.Log("Interaction!");
        if (Interactions != null)
        {
            Interactions(target);
        }
        if (gameEvent != null)
        {
            UI_RuntimeLog.instance.CallLog(gameEvent.ToString());
            GameEventExecutor.instance.Excute(gameEvent);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //interaction.enabled = true;
            if (type == Type.contact)
            {
                Interacting(collision.gameObject);
            }
            else
            {
                PlayerManager.instance.controller.interactiveTarget = this;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerManager.instance.controller.interactiveTarget == this)
        {
            //interaction.enabled = false;
            PlayerManager.instance.controller.interactiveTarget = null;
        }
    }

}
