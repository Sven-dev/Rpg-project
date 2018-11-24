using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInteracter : MonoBehaviour {

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
        M.OnMovementChange += SetInteractTrigger;
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
                Interact(Objects[0]);
                return;
            }

            Attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable i = collision.GetComponent<Interactable>();
        if (i != null)
        {
            Objects.Add(i);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable i = collision.GetComponent<Interactable>();
        if (i != null)
        {
            Objects.Remove(i);
        }
    }

    void Attack()
    {
        if (!Attacking2)
        {
            if (!Attacking1)
            {
                StartCoroutine(_attack());
            }
            else if (Attack2Ready)
            {
                StartCoroutine(_attack2());
            }
        }
    }

    IEnumerator _attack()
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

    IEnumerator _attack2()
    {
        Attacking2 = true;
        M.Immobile = true;
        OnAttackChange();

        //Cycle through all attack-related colliders, enabling and disabling them in order to emulate a slash
        for (int i = Colliders.Count -1; i >= 0; i--)
        {
            Colliders[i].enabled = true;
            if (i == 0)
            {
                Attack3Ready = true;
            }

            yield return new WaitForSeconds(0.05f);
            Colliders[i].enabled = false;
        }

        M.Immobile = false;
        yield return new WaitForSeconds(0.15f);
        Attack3Ready = false;

        Attacking2 = false;
    }

    void Interact(Interactable obj)
    {
        M.Immobile = true;
        obj.Interact();
    }

    //Rotates the interact-trigger, facing it in the direction the object is heading
    public void SetInteractTrigger()
    {
        switch(M.Direction)
        {
            case Direction.Up:
                transform.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case Direction.Down:
                transform.localEulerAngles = new Vector3(0, 0, -90);
                break;
            case Direction.Left:
                transform.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case Direction.Right:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case Direction.UpLeft:
                transform.localEulerAngles = new Vector3(0, 0, 135);
                break;
            case Direction.UpRight:
                transform.localEulerAngles = new Vector3(0, 0, 45);
                break;
            case Direction.DownLeft:
                transform.localEulerAngles = new Vector3(0, 0, -135);
                break;
            case Direction.DownRight:
                transform.localEulerAngles = new Vector3(0, 0, -45);
                break;
        }
    }
}
