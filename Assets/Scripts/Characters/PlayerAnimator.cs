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
        Attacker.OnAttackLaunch += AttackAnimation;
    }

    //Make the player walk or stand still
    protected override void WalkAnimation(Direction direction, bool idle)
    {
        string animationType = "";
        //If the player doesn't have a sword
        if (!Attacker.HasSword)
        {
            base.WalkAnimation(direction, idle);
            return;
        }
        //If the player is charging an attack
        else if (Attacker.WindingUp)
        {
            animationType += "Charge_";
        }
        else
        {
            animationType += "Armed_";
        }

        if (idle)
        {
            animationType += "Idle_";
        }
        else
        {
            animationType += "Walk_";
        }

        animationType += direction.ToString();
        Animator.Play(animationType);
    }
    //Plays the attack animation
    private void AttackAnimation(Direction direction)
    {
        Animator.Play("Attack_" + direction.ToString());
    }
}