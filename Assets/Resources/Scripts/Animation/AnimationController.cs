using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public delegate void Action_AfterAnimation();
    public Action_AfterAnimation actions;
    public bool actionReady = false;

    public void setTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void setInt(string name, int value)
    {
        animator.SetInteger(name, value);
    }
    public void setFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    public bool isEnd(string AnimationName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer."+AnimationName))
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
        else return false;
    }
}
