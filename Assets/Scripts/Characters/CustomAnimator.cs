using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    protected Movement M;
    protected Animator A; 

    protected void Start()
    {
        M = GetComponent<Movement>();
        A = GetComponent<Animator>();

        M.OnMovementChange += DirectionToClip;
    }

    //Selects and plays a movement-animation (walking or idle)
    void DirectionToClip()
    {
        if (M.Idle)
        {
            A.Play(M.Direction.ToString() + "_Idle");
        }
        else
        {
            A.Play(M.Direction.ToString() + "_Walk");
        }
    }
}