using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : Animations {

    AttackInteracter AI;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
        AI = transform.GetComponentInChildren<AttackInteracter>();
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
}