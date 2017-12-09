using UnityEngine;

public class Lever : Interactable
{
    public Sprite On;
    public Sprite Off;

    public bool State;

    public GameObject Controller;
    private Controller ControllerScript;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start()
    {
        ControllerScript = Controller.GetComponent<Controller>();
        sr = GetComponent<SpriteRenderer>();

        UpdateSprite();
    }
	
	// Update is called once per frame
	void Update()
    {   //If the levers has all variables
        if (Controller != null)
        {
            //if the lever is turned on
            if (State == true)
            {
                ControllerScript.CheckLogic();
            }
        }
	}

    //Turns the Lever OFF or ON
    void ToggleState()
    {
        State = !State;
    }

    //Updates the sprite of the lever
    void UpdateSprite()
    {
        if (State == true)
        {
            sr.sprite = On;
        }
        else
        {
            sr.sprite = Off;
        }
    }

    //Methods for objects to interact with the lever
    public override void Interact()
    {
        ToggleState();
        UpdateSprite();
    }
}
