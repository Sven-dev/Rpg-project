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

        Movement.OnMovementChange += DirectionToClip;
    }

    //Selects and plays a movement-animation (walking or idle)
    void DirectionToClip(Direction d)
    {
        if (Movement.Idle)
        {
            Animator.Play(d.ToString() + "_Idle");
        }
        else
        {
            Animator.Play(d.ToString() + "_Walk");
        }
    }
}