using UnityEngine;
using System.Collections;

public class TrialDoor : MonoBehaviour
{
    public bool State;

    public AnimationClip Open_anim;
    public AnimationClip Close_anim;

    public Sprite HeadOn;
    public Sprite HeadOff;

    private SpriteRenderer head;
    public BoxCollider doorCollider;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        head = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
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
    public void UpdateSprite()
    {
        Debug.Log("Updating");
        if (State == true)
        {
            anim.Play(Open_anim.name);
            head.sprite = HeadOn;

            doorCollider.enabled = false;
        }
        else
        {
            anim.Play(Close_anim.name);
            head.sprite = HeadOff;

            doorCollider.enabled = true;
        }
    }
}
