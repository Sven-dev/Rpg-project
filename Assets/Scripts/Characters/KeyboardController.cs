using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;
    AttackInteracter IA;
    ConversationManager CM;
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
        M = GetComponent<Movement>();
        IA = transform.GetChild(0).GetComponent<AttackInteracter>();
        CM = GetComponent<ConversationManager>();
    }

    //Handles any non physics-related inputs (like menus)
    private void Update()
    {
        if (Active)
        {
            if (Input.GetKeyDown(Global.Keys.Attack_Interact))
            {
                if (CM.Active)
                {
                    CM.FinishTalking();
                }
                else
                {
                    IA.CheckForInteract();
                }
            }
            else if (Input.GetKeyDown(Global.Keys.Right))
            {
                if (CM.Active)
                {
                    CM.MoveCursor(1);
                }
            }
            else if (Input.GetKeyDown(Global.Keys.Left))
            {
                if (CM.Active)
                {
                    CM.MoveCursor(-1);
                }
            }
        }
    }

    //Handles any physics-related inputs (FixedUpdate runs at the same rate as the physics-engine)
    private void FixedUpdate()
    {

        if (Active)
        {
            if (Input.GetKey(Global.Keys.Up) || Input.GetKey(Global.Keys.Left) || Input.GetKey(Global.Keys.Down) || Input.GetKey(Global.Keys.Right))
            {
                Direction facing = Direction.Null;
                if (Input.GetKey(Global.Keys.Up))
                {
                    facing = Direction.Up;
                    M.Move(Direction.Up);
                }
                if (Input.GetKey(Global.Keys.Down))
                {
                    facing = Direction.Down;
                    M.Move(Direction.Down);
                }
                if (Input.GetKey(Global.Keys.Left))
                {
                    facing = Direction.Left;
                    M.Move(Direction.Left);
                }
                if (Input.GetKey(Global.Keys.Right))
                {
                    facing = Direction.Right;
                    M.Move(Direction.Right);
                }

                if (facing != Direction.Null)
                {
                    M.Direction = facing;
                }

                if (M.Idle == true)
                {
                    M.Idle = false;
                }
            }
            else if (M.Idle == false)
            {
                M.Idle = true;
            }
        }
    }
}