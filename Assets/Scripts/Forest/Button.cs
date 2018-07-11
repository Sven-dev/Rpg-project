using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : Interactable {

    public Sprite On;
    public Sprite Off;

    public bool State;

    public List<TrialDoor> DoorList;
    public Controller ControllerScript;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //Methods for objects to interact with the lever
    public override void Interact()
    {
        Press();
    }

    //Turns on the button, Changes the sprite and starts a timer for changing it back
    public void Press()
    {
        if (State != true)
        {
            State = true;
            sr.sprite = On;

            if (ControllerScript is DoorController)
            {
                DoorController logic = ControllerScript as DoorController;
                logic.CurrentButton = this;
                ControllerScript.CheckLogic();
            }
        }

        //GameObject.FindWithTag("Player").GetComponent<Player>().Controls_ON();
    }

    public void TurnOff()
    {
        State = false;
        sr.sprite = Off;
    }
}