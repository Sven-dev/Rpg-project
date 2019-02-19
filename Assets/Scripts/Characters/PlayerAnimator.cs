using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CustomAnimator
{
    private bool WindingUp;

	// Use this for initialization
	new void Start ()
    {
        base.Start();
        WindingUp = false;

        Attacker Attacker = transform.GetChild(1).GetComponent<Attacker>();
        Attacker.OnAttackWindUp += SetWindUpState;
        Attacker.OnAttackLaunch += AttackAnimation;
    }

    //Make the player walk or stand still
    protected override void WalkAnimation(Direction direction, bool idle)
    {
        if (WindingUp)
        {
            WindUpAnimation(direction, idle);
        }
        else
        {
            base.WalkAnimation(direction, idle);
        }
    }

    //Sets the windup state
    private void SetWindUpState(bool value)
    {
        WindingUp = value;
    }

    //Make the player walk or stand still while charging their attack
    private void WindUpAnimation(Direction direction, bool idle)
    {
        if (idle)
        {
            Animator.Play("Charge_" + direction.ToString() + "_Idle");
        }
        else
        {
            Animator.Play("Charge_" + direction.ToString() + "_Walk");
        }
    }

    //Plays the attack animation
    private void AttackAnimation(Direction direction)
    {
        Animator.Play("Attack_" + direction.ToString());
    }
}