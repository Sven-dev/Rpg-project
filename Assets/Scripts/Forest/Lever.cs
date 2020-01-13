using UnityEngine;
using System.Collections;

public class Lever : Interactable
{
    public Sprite On;
    public Sprite Off;

    private bool State;
    private SpriteRenderer sr;

    public delegate void StateChange(bool state);
    public StateChange OnStateChange;

	/// <summary>
    /// Gets the reference to the spriterenderer
    /// </summary>
	void Start()
    {
        sr = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Toggle(true);
    }

    /// <summary>
    /// turns the lever on or off
    /// </summary>
    private void Toggle()
    {
        // Update variables
        State = !State;
        OnStateChange(State);

        // Update sprite
        if (State == true)
            sr.sprite = On;
        else
            sr.sprite = Off;
    }

    /// <summary>
    /// Turns the lever on or off
    /// </summary>
    /// <param name="state">the state the lever gets set to</param>
    private void Toggle(bool state)
    {
        // Update variables
        State = state;
        OnStateChange(State);

        // Update sprite
        if (State == true)
            sr.sprite = On;
        else
            sr.sprite = Off;
    }

    /// <summary>
    /// Methods for objects to interact with the lever
    /// </summary>
    public override void Interact()
    {
        Toggle();
    }
}