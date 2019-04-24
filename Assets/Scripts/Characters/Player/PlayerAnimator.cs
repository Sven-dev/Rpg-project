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
        Attacker.OnAttackLaunch += Attack;
    }

    //Make the player walk or stand still
    protected override void WalkAnimation(Direction direction, bool idle)
    {
        string animation = "";
        //If the player doesn't have a sword
        if (!Attacker.HasSword)
        {
            base.WalkAnimation(direction, idle);
            return;
        }

        if (Attacker.HasSword)
        {
            animation += "Armed_";
        }

        if (idle)
        {
            animation += "Idle_";
        }
        else
        {
            animation += "Walk_";
        }

        animation += direction.ToString();
        Animator.Play(animation);
    }
    //Plays the attack animation
    private void Attack(Direction direction)
    {
        StartCoroutine(_Attack(direction));
    }

    IEnumerator _Attack(Direction direction)
    {
        Animator.Play("Attack_" + direction.ToString());
        yield return new WaitForSeconds(0.75f);
        Animator.Play("Armed_Idle_" + direction);
    }
}