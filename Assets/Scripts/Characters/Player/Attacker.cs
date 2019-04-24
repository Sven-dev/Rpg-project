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

    public delegate void AttackLaunch(Direction direction);
    public event AttackLaunch OnAttackLaunch;

    private void Start()
    {
        Movement = transform.parent.GetComponent<Movement>();

        Hitboxes = GetComponents<Collider2D>().ToList();
        print(Hitboxes.Count);
    }

    //Launch the attack
    public void Attack()
    {
        StartCoroutine(_Attack());
    }

    private IEnumerator _Attack()
    {
        Movement.Immobile = true;
        Attacking = true;
        OnAttackLaunch(Movement.Direction);

        Hitboxes[0].enabled = true;
        yield return new WaitForSeconds(0.25f);
        Hitboxes[0].enabled = false;
        Hitboxes[1].enabled = true;
        yield return new WaitForSeconds(0.25f);
        Hitboxes[1].enabled = false;
        Hitboxes[2].enabled = true;
        yield return new WaitForSeconds(0.25f);
        Hitboxes[2].enabled = false;

        Attacking = false;
        Movement.Immobile = false;
    }
}