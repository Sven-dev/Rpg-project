using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInteracter : MonoBehaviour
{
    public bool Attacking;

    Movement M;
    List<Interactable> Objects;


    bool Attack2Ready;
    bool Attack3Ready;

    [HideInInspector]
    public bool Attacking1;
    [HideInInspector]
    public bool Attacking2;

    List<BoxCollider2D> Colliders;

    public delegate void AttackInteracterChange();
    public event AttackInteracterChange OnAttackChange;

    // Use this for initialization
    void Start ()
    {
        M = transform.parent.GetComponent<Movement>();
        //M.OnMovementChange += SetInteractTrigger;
        Objects = new List<Interactable>();

        bool Attack2Ready = false;
        bool Attacking1 = false;
        bool Attacking2 = false;

        Colliders = new List<BoxCollider2D>();
        Colliders.AddRange(transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>());
    }

    public void CheckForInteract()
    {
        if (!M.Immobile)
        {
            if (Objects.Count > 0)
            {
                //Interact(Objects[0]);
                return;
            }

            Attack();
        }
    }

    void Attack()
    {
        if (!Attacking2)
        {
            if (!Attacking1)
            {
                StartCoroutine(_Attack());
            }
            else if (Attack2Ready)
            {
                //StartCoroutine(_attack2());
            }
        }
    }

    /*
    //Winds up the attack animation
    IEnumerator WindUp()
    {
        //While the attack button is pressed, charge the attack
        while()
        {

        }
        //If the attack button is let go of before the attack is charged
        //return to the walking animation
        //Attack
    }
    */

    IEnumerator _Attack()
    {
        Attacking1 = true;
        M.Immobile = true;
        OnAttackChange();

        yield return new WaitForSeconds(0.1f);

        //Cycle through all attack-related colliders, enabling and disabling them in order to emulate a slash
        for (int i = 0; i < Colliders.Count; i++)
        {
            Colliders[i].enabled = true;
            if (i == 3)
            {
                Attack2Ready = true;
            }

            yield return new WaitForSeconds(0.025f);
            Colliders[i].enabled = false;
        }


        yield return new WaitForSeconds(0.3f);
        Attack2Ready = false;
        Attacking1 = false;
        M.Immobile = false;
    }
}
