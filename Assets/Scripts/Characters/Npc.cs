using UnityEngine;
using System.Collections;

public class Npc : Interactable {

    public float Speed;
    private string Direction;
    public DialogueHandler dialogue;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("D_Idle");
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public override void Interact()
    {
        StartCoroutine(interaction());
    }

    IEnumerator interaction()
    {
        Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
        TurnToPlayer(p);

        yield return new WaitForEndOfFrame();
        dialogue.StartProcess();

        while (dialogue.Active)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("controls on");
        p.Controls_ON();
    }

    //Makes the npc turn to the player when talked to
    void TurnToPlayer(Player p)
    {
        string dir = p.GetDirection();

        if(dir == "W")
        {
            dir = "S";
        }
        else if (dir == "A")
        {
            dir = "D";
        }
        else if (dir == "S")
        {
            dir = "W";
        }
        else //if (dir == "D")
        {
            dir = "A";
        }

        anim.Play(dir + "_Idle");
    }

    //Lets an NPC walk to a position, as long as it is a straight line, and there's no object in between.
    void MoveTo(Vector2 pos)
    {

    }

    //Makes the npc move
    void Move(Vector3 direction)
    {
        transform.position += (direction * Speed) * Time.fixedDeltaTime;
        //SetSortingLayer();
    }

    //direction
    //Movement
    //talking
}
