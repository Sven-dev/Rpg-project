using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : Interactable {

    public Sprite On;
    public Sprite Off;

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
    void Press()
    {
        if (sr.sprite != On)
        {
            sr.sprite = On;
            if (ControllerScript is DoorController)
            {
                DoorController logic = ControllerScript as DoorController;
                logic.CurrentButton = this;
                ControllerScript.CheckLogic();
            }
        }

        GameObject.FindWithTag("Player").GetComponent<Player>().Controls_ON();
    }

    public void TurnOff()
    {
        sr.sprite = Off;
        foreach (TrialDoor Door in DoorList)
        {
            Door.Close();
        }
    }
}