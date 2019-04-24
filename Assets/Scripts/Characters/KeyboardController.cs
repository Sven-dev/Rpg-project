using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    private Movement Movement;
    private ConversationManager ConversationManager;
    private Attacker Attacker;
    private Interacter Interacter;

    public bool Active;
    private bool FirstFrame = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        Active = true;
        Movement = GetComponent<Movement>();
        ConversationManager = GetComponent<ConversationManager>();
        Interacter = transform.GetChild(0).GetComponent<Interacter>();
        Attacker = transform.GetChild(1).GetComponent<Attacker>();
    }

    //Handles any non physics-related inputs (like menus)
    private void Update()
    {
        //Dialogue controls
        if (ConversationManager.Active)
        {
            //Move the selected index right
            if (Input.GetKeyDown(Global.Keys.Right))
            {
                ConversationManager.MoveCursor(1);
            }
            //Move the selected index left
            else if (Input.GetKeyDown(Global.Keys.Left))
            {
                ConversationManager.MoveCursor(-1);
            }

            //Advance the dialogue
            if (Input.GetKeyDown(Global.Keys.Attack_Interact))
            {
                ConversationManager.FinishTalking();
            }
        }
        else
        {
            //Check if the interact/attack button is held down
            if (!Movement.Immobile && !Attacker.Attacking && Input.GetKey(Global.Keys.Attack_Interact))
            {
                //If the player is looking at an interactable object
                if (Interacter.Interactable())
                {
                    Interacter.Interact();
                }
                //If the player has a sword
                else if (Attacker.HasSword)
                {
                    Attacker.Attack();
                }
            }
        }
    }

    //Handles any physics-related inputs (FixedUpdate runs at the same rate as the physics-engine)
    private void FixedUpdate()
    {
        if (!Attacker.Attacking)
        {
            //If any of the directional inputs is pressed
            if (Input.GetKey(Global.Keys.Up) || Input.GetKey(Global.Keys.Left) || Input.GetKey(Global.Keys.Down) || Input.GetKey(Global.Keys.Right))
            {
                Direction facing = Direction.Null;

                //If up, move up
                if (Input.GetKey(Global.Keys.Up))
                {
                    facing = Direction.Up;
                    Movement.Move(Direction.Up);
                }

                //If down, move down
                if (Input.GetKey(Global.Keys.Down))
                {
                    facing = Direction.Down;
                    Movement.Move(Direction.Down);
                }

                //If left, move left
                if (Input.GetKey(Global.Keys.Left))
                {
                    facing = Direction.Left;
                    Movement.Move(Direction.Left);
                }

                //If right, move right
                if (Input.GetKey(Global.Keys.Right))
                {
                    facing = Direction.Right;
                    Movement.Move(Direction.Right);
                }

                //If the player was idle, don't be
                if (Movement.Idle == true)
                {
                    Movement.Idle = false;
                }

                //if the player was facing a different direction, set it
                if (facing != Direction.Null && facing != Movement.Direction)
                {
                    Movement.Direction = facing;
                }
            }
            else if (Movement.Idle == false)
            {
                Movement.Idle = true;
            }
        }    
    }
}