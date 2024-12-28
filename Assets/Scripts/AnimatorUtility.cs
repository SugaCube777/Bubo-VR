using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorUtility : MonoBehaviour
{
    Animator animator;

    public void SetBoolTrue(string name)
    {
        checkAnimator();
        animator.SetBool(name, true);
    }

    public void SetBoolFalse(string name)
    {
        checkAnimator();
        animator.SetBool(name, false);
    }

    void checkAnimator()
    {
        if (!animator)
            animator = GetComponent<Animator>();
    }

}
