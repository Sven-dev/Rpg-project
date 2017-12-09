using UnityEngine;
using System.Collections;

public class Button : Interactable {

    public Sprite On;
    public Sprite Off;

    public int OnTime;
    private int CurrentTime;

    public GameObject Controller;
    private Controller ControllerScript;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start ()
    {
        ControllerScript = Controller.GetComponent<Controller>();
        sr = GetComponent<SpriteRenderer>();

        CurrentTime = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //If there is 1 second left on the timer, change the sprite to off
        if (CurrentTime == 0)
        {
            sr.sprite = Off;
        }

        //If the timer is going, make sure it counts down (per frame)
	    if (CurrentTime >= 0)
        {
            CurrentTime--;
        }
	}

    //Methods for objects to interact with the lever
    public override void Interact()
    {
        ToggleState();
    }

    //Turns on the button, Changes the sprite and starts a timer for changing it back
    void ToggleState()
    {
        ControllerScript.CheckLogic();
        sr.sprite = On;
        CurrentTime = OnTime;
    }
}
