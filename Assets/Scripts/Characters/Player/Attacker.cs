using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Attacker : MonoBehaviour
{
    public bool HasSword;
    public bool Attacking;
    private Movement Movement;

    private List<Collider2D> Hitboxes;

    public delegate void AttackLaunch();
    public event AttackLaunch OnAttack;

    private void Start()
    {
        Movement = transform.parent.GetComponent<Movement>();

        Hitboxes = GetComponents<Collider2D>().ToList();
    }

    //Launch the attack
    public void Attack()
    {
        StartCoroutine(_Attack());
    }

    private IEnumerator _Attack()
    {
        Attacking = true;

        yield return new WaitForSeconds(0.015f);
        OnAttack();

        yield return new WaitForSeconds(0.25f);
        Hitboxes[0].enabled = true;
        yield return new WaitForSeconds(0.05f);
        Hitboxes[0].enabled = false;
        Hitboxes[1].enabled = true;
        yield return new WaitForSeconds(0.05f);
        Hitboxes[1].enabled = false;
        Hitboxes[2].enabled = true;
        yield return new WaitForSeconds(0.05f);
        Hitboxes[2].enabled = false;
        yield return new WaitForSeconds(0.25f);

        Attacking = false;
    }
}