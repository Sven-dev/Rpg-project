using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    protected Movement Movement;
    protected Animator Animator; 

    protected void Start()
    {
        Movement = GetComponent<Movement>();
        Animator = GetComponent<Animator>();

        Movement.OnMovementChange += WalkAnimation;
    }

    //Selects and plays a movement-animation (walking or idle)
    protected virtual void WalkAnimation(Direction direction, bool idle)
    {
        if (Movement.Idle)
        {
            Animator.Play(direction.ToString() + "_Idle");
        }
        else
        {
            Animator.Play(direction.ToString() + "_Walk");
        }
    }
}