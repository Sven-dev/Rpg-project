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
        //Movement.OnDirectionChange += SetDirection;
        //Movement.OnIdleChange += SetIdle;
    }

     protected virtual void SetDirection(Direction direction)
    {
        Animator.SetInteger("Direction", (int)direction);
    }   
    
    protected virtual void SetIdle(bool idle)
    {
        Animator.SetBool("Idle", idle);
    }
}