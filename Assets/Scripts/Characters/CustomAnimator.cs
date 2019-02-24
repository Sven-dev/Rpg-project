using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    protected Animator Animator; 

    protected void Start()
    {
        Animator = GetComponent<Animator>();
        Movement Movement = GetComponent<Movement>();
        Movement.OnMovementChange += WalkAnimation;
    }

    //Selects and plays a movement-animation (walking or idle)
    protected virtual void WalkAnimation(Direction direction, bool idle)
    {
        if (idle)
        {
            Animator.Play("Idle_" + direction.ToString());
        }
        else
        {
            Animator.Play("Walk_" + direction.ToString());
        }
    }
}