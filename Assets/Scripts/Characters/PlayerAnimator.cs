using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CustomAnimator
{
    AttackInteracter AttackInteracter;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
        //AttackInteracter = transform.GetComponentInChildren<AttackInteracter>();
        //AttackInteracter.OnAttackChange += AttackToClip;
    }

    //Selects and plays an attack-animation
    void AttackToClip()
    {
        int AtkNr;
        if (AttackInteracter.Attacking1)
        {
            AtkNr = 1;
        }
        else //if (AI.Attacking2)
        {
            AtkNr = 2;
        }

        Animator.Play(Movement.Direction.ToString() + "_Attack_" + AtkNr);
    }
}