﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    #region Fields
    public bool Balancing;
    private string Direction;
    private Vector3 Deltaposition;
    public bool ControlsLocked;

    private Animator anim;
    private Movement mover;
    #endregion

    #region Unity Logic
    void Start()
    {
        //Makes sure the object doesn't unload when switching scenes
        DontDestroyOnLoad(transform.gameObject);

        mover = GetComponent<Movement>();

        //Gets the animator and sets a default animation and direction
        anim = GetComponent<Animator>();
        anim.Play("D_Idle");
        Direction = "D";

        //Unlocks the controls (they never ""should"" be, but just in case)
        ControlsLocked = false;
	}

    void Update()
    {
        CheckForInteract();
    }
    #endregion

    #region Movement
    void updateSprite(bool Walking)
    {
        if (Balancing)
        {
            if (Walking)
            {
                anim.Play("Balance_" + Direction + "_Walk");
            }
            else
            {
                anim.Play("Balance_" + Direction + "_Idle");
            }
        }
        else // if regular walking
        {
            if (Walking)
            {
                anim.Play(Direction + "_Walk");
            }
            else
            {
                anim.Play(Direction + "_Idle");
            }
        }
    }
    #endregion

    #region Interacting
    public string GetDirection()
    {
        return Direction;
    }

    void CheckForInteract()
    {
        //Checks if z or enter is pressed (interaction)
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!ControlsLocked)
            {
                InteractWithObject();
            }
        }
    }

    //Uses a raycast to check in front of the player for an interactable object, and interacts with it
    void InteractWithObject()
    {
        Vector3 pos = transform.position;
        pos += Vector3.down;

        Vector3 dir = GetDirectionVector();
        float dis = 1f;

        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, dis))
        {
            Interactable Object = hit.transform.gameObject.GetComponent<Interactable>();
            if (Object != null)
            {
                Object.Interact();
            }
        }
    }
    #endregion

    #region Trigger detection
    //Checsks if the player is standing on a trigger, and activates the trigger
    void OnTriggerEnter(Collider other)
    {
        Trigger T = other.GetComponent<Trigger>();

        if (T != null)
        {
            T.ExecuteTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Trigger T = other.GetComponent<Trigger>();

        if (T != null)
        {
            T.ExitTrigger();
        }
    }
    #endregion

    #region Toggle Movement
    //Locks the player's controls in case they're in a cutscene or in dialogue
    public void ControlsToggle()
    {
        ControlsLocked = !ControlsLocked;
    }
    public void Controls_OFF()
    {
        ControlsLocked = true;
    }

    public void Controls_ON()
    {
        ControlsLocked = false;
    }
    #endregion

    #region Misc.

    //Takes a direction (W, A, S, D) and returns a vector3
    Vector3 GetDirectionVector()
    {
        if(Direction == "W")
        {
            return Vector3.up;
        }
        else if (Direction == "A")
        {
            return Vector3.left;
        }
        else if (Direction == "S")
        {
            return Vector3.down;
        }
        else if (Direction == "D")
        {
            return Vector3.right;
        }
        else
        {
            throw new System.Exception();
        }
    }
    #endregion
}
