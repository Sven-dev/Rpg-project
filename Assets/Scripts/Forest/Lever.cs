using UnityEngine;
using System.Collections;

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
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        UpdateSprite();
    }


    IEnumerator State_ON()
    {
        while (State == true)
        {
            ControllerScript.CheckLogic();
            yield return null;
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
            StartCoroutine(State_ON());
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
