using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Movement M;
    AttackInteracter AI;
    Animator A; 

    private void Start()
    {
        M = GetComponent<Movement>();
        AI = transform.GetComponentInChildren<AttackInteracter>();
        A = GetComponent<Animator>();

        M.OnDirectionChange += DirectionToClip;
        AI.OnAttackChange += AttackToClip;
    }

    //Selects and plays an attack-animation
    void AttackToClip()
    {
        int AtkNr;
        if (AI.Attacking1)
        {
            AtkNr = 1;
        }
        else //if (AI.Attacking2)
        {
            AtkNr = 2;
        }

        A.Play(M.Direction.ToString() + "_Attack_" + AtkNr);
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