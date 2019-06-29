using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CustomAnimator
{
    private Attacker Attacker;

	// Use this for initialization
	new void Start ()
    {
        base.Start();

        Attacker = transform.GetChild(1).GetComponent<Attacker>();
        Attacker.OnAttack += Attack;
    }

    //Plays the attack animation
    private void Attack()
    {
        Animator.SetTrigger("Attack");
    }
}