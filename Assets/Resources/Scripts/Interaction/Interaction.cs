using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public enum Type {trigger, contact}
    public enum keyType { select, DirectionKey }
    public Type type = Type.trigger;
    public keyType key = keyType.select;
    public bool exectueAfterAnimation;
    public delegate void interact(GameObject target);
    public interact Interactions;
    public float inputValue = 0;

    public void Interacting(GameObject target)
    {
        Interactions(target);
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
                PlayerManager.instanse.controller.interactiveTarget = this;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PlayerManager.instanse.controller.interactiveTarget == this)
        {
            //interaction.enabled = false;
            PlayerManager.instanse.controller.interactiveTarget = null;
        }
    }

}
