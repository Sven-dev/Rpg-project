using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackInteracter : MonoBehaviour {

    Movement M;
    List<Interactable> Objects;

    bool Attack2Ready;
    bool Attack3Ready;
    bool Attacking1;
    bool Attacking2;
    bool Attacking3;
    List<BoxCollider> Colliders;

	// Use this for initialization
	void Start ()
    {
        M = transform.parent.GetComponent<Movement>();
        M.OnAnimationChange += SetInteractTrigger;
        Objects = new List<Interactable>();

        bool Attack2Ready = false;
        bool Attack3Ready = false;
        bool Attacking1 = false;
        bool Attacking2 = false;
        bool Attacking3 = false;

        Colliders = new List<BoxCollider>();
        Colliders.AddRange(transform.GetChild(0).GetComponentsInChildren<BoxCollider>());
    }

    public void CheckForInteract()
    {
        if (Objects.Count > 0)
        {
            Interact(Objects[0]);
            return;
        }

        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i != null)
        {
            Objects.Add(i);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i != null)
        {
            Objects.Remove(i);
        }
    }

    void Attack()
    {
        if (!Attacking3)
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
            else if (Attack3Ready)
            {
                StartCoroutine(_attack3());
            }
        }
    }

    IEnumerator _attack()
    {
        Attacking1 = true;

        //Cycle through all attack-related colliders, enabling and disabling them in order to emulate a slash
        for (int i = 0; i < Colliders.Count; i++)
        {
            Colliders[i].enabled = true;
            if (i == 3)
            {
                Attack2Ready = true;
            }

            yield return new WaitForSeconds(0.05f);
            Colliders[i].enabled = false;
        }

        yield return new WaitForSeconds(0.3f);
        Attack2Ready = false;

        Attacking1 = false;
    }

    IEnumerator _attack2()
    {
        Attacking2 = true;

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

        yield return new WaitForSeconds(0.35f);
        Attack3Ready = false;

        Attacking2 = false;
    }

    IEnumerator _attack3()
    {
        Attacking3 = true;

        yield return new WaitForSeconds(0.1f);

        Colliders[1].enabled = true;
        Colliders[2].enabled = true;
        yield return new WaitForSeconds(0.1f);
        Colliders[1].enabled = false;
        Colliders[2].enabled = false;

        yield return new WaitForSeconds(0.3f);
        Attacking3 = false;
    }

    void Interact(Interactable obj)
    {
        print("Interact");
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
