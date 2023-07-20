using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationKeyEventMethods : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #region Animation Event Key Methods
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
