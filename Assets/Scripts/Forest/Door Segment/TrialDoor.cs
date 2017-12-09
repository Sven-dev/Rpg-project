using UnityEngine;
using System.Collections;

public class TrialDoor : MonoBehaviour
{
    public bool State;

    public Sprite On;
    public Sprite Off;

    public AnimationClip Open_anim;
    public AnimationClip Close_anim;

    public Sprite HeadOn;
    public Sprite HeadOff;

    private SpriteRenderer head;
    public BoxCollider doorCollider;
    private Animator an;

    // Use this for initialization
    void Start()
    {
        head = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider>();
        an = GetComponent<Animator>();

        UpdateStart();
    }
	
    //Opens or closes the door
    public void Toggle()
    {
        State = !State;
        UpdateSprite();
    }

    public void Open()
    {
        State = true;
        UpdateSprite();
    }

    public void Close()
    {
        State = false;
        UpdateSprite();
    }

    //Updates the sprite and collision of the Door
    void UpdateSprite()
    {
        if (State == true)
        {   
            an.Play(Open_anim.name);
            head.sprite = HeadOn;
            doorCollider.enabled = false;
        }
        else
        {
            an.Play(Close_anim.name);
            head.sprite = HeadOff;
            doorCollider.enabled = true;
        }
    }

    //Updates the sprite and collision of the Door for on start, without the animations
    void UpdateStart()
    {
        if (State == true)
        {
            head.sprite = HeadOn;
            doorCollider.enabled = false;
        }
        else
        {
            head.sprite = HeadOff;
            doorCollider.enabled = true;
        }
    }
}
