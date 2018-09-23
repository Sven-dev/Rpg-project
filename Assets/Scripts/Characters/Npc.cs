using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Interactable {

    private Vector3 Deltaposition;
    private string Direction;

    public List<Action> ActionList;

    private bool active;
    private int currentIndex;

    private Animator anim;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        Deltaposition = transform.position;
        Direction = "D";
        anim.Play("D_Idle");
    }

    void Update()
    {
        if (Deltaposition != transform.position)
        {
            SetDirection();
        }
        else
        {
            anim.Play(Direction + "_Idle");
        }

        Deltaposition = transform.position;
    }
	
    public override void Interact()
    {
        StartCoroutine(interaction());
    }

    IEnumerator interaction()
    {
        TurnTo(Player);

        //Starts the first action in the list
        this.active = true;
        ActionList[0].StartProcess(Player);

        while (active)
        {
            if (ActionList[currentIndex].Active == false)
            {
                nextaction();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //Makes the npc turn the opposite direction of the target,
    //making it look like the object turned to the object
    void TurnTo(GameObject target)
    {
        //string playerdir = p.GetDirection();
        /*
        if(playerdir == "W")
        {
            Direction = "S";
        }
        else if (playerdir == "A")
        {
            Direction = "D";
        }
        else if (playerdir == "S")
        {
            Direction = "W";
        }
        else //if (dir == "D")
        {
            Direction = "A";
        }

        anim.Play(Direction + "_Idle");
        */
    }

    void SetDirection()
    {
        if (Deltaposition.y < transform.position.y) //move up
        {
            Direction = "W";
        }

        if (Deltaposition.y > transform.position.y) //move down
        {
            Direction = "S";
        }

        if (Deltaposition.x > transform.position.x) //move left
        {
            Direction = "A";
        }

        if (Deltaposition.x < transform.position.x) //move right
        {
            Direction = "D";
        }

        anim.Play(Direction + "_Walk");
    }

    void nextaction()
    {
        if (currentIndex < ActionList.Count - 1)
        {
            currentIndex++;
            ActionList[currentIndex].StartProcess(Player);
        }
        else
        {
            this.active = false;
        }
    }
}
