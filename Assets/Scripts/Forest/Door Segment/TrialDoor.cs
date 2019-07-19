using UnityEngine;
using System.Collections;

public class TrialDoor : MonoBehaviour
{
    public bool toggle;
    public bool Opened;
    private bool State
    {
        get { return Opened; }
        set
        {
            Opened = value;
            Doorway.enabled = !value;
            Animator.SetBool("State", value);
        }
    }

    private BoxCollider2D Doorway;
    private Animator Animator;

    // Use this for initialization
    void Start()
    {
        Doorway = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
        Animator.SetBool("State", Opened);
    }

    private void Update()
    {
        if (toggle)
        {
            Toggle();
            toggle = false;
        }
    }

    //Opens or closes the door
    public void Toggle()
    {
        State = !State;
    }

    public void Open()
    {
        State = true;
    }

    public void Close()
    {
        State = false;
    }
}
