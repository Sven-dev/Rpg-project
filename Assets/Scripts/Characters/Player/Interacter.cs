using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private Movement Movement;
    private Interactable Selected;

	// Use this for initialization
	void Start ()
    {
        Movement = transform.parent.GetComponent<Movement>();
	}

    public void Interact()
    {
        Selected.Interact();
    }

    //Checks if there's an object in front of the player they can interact with
    public bool Interactable()
    {
        //If the player has anything interactable in front of them
        if (Selected != null)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Is the collided object interactable?
        Interactable i = collision.GetComponent<Interactable>();
        if (i != null)
        {
            //If there's no already selected object, select this one
            if (Selected == null)
            {
                Selected = i;
                //Selected.Select();
            }
            else
            {
                //Check what object is closest to the player
                float currentdistance = Vector2.Distance(Selected.transform.position, Movement.transform.position);
                float newdistance = Vector2.Distance(i.transform.position, Movement.transform.position);

                //if the new object is closer
                if (currentdistance > newdistance)
                {
                    //Select the new object
                    //Selected.Deselect();
                    Selected = i;
                    //Selected.Select();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Is the collided object interactable?
        Interactable i = collision.GetComponent<Interactable>();
        if (i != null && i == Selected)
        {
            Selected.Deselect();
            Selected = null;
        }
    }
}