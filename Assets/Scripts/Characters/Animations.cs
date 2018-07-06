using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Movement M;
    Animator A; 

    private void Start()
    {
        A = GetComponent<Animator>();
        M = GetComponent<Movement>();
        M.OnAnimationChange += SetAnimation;
    }

    //Animates the object
    private void SetAnimation()
    {
        //if attacking

        //else
        DirectionToClip();
    }

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