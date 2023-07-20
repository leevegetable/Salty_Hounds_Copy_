using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StackItem : MonoBehaviour
{
    public bool BlockEscapeKey = false;
    private void OnEnable()
    {
        UIManager.instance.stackItems.Add(this);
        PlayerManager.instanse.controller.isControl = false;
    }
    private void OnDisable()
    {
        if(UIManager.instance.stackItems.Contains(this))
        {
            UIManager.instance.stackItems.Remove(this);
        }
    }
}
