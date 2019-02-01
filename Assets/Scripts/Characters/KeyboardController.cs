using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;
    AttackInteracter IA;
    ConversationManager CM;
    public bool Active;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);        
    }

    void Start()
    {
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
            if (Input.GetKeyDown(KeyCode.Space))
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
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (CM.Active)
                {
                    CM.MoveCursor(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
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
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                Direction facing = Direction.Null;
                if (Input.GetKey(KeyCode.W))
                {
                    facing = Direction.Up;
                    M.Move(Direction.Up);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    facing = Direction.Down;
                    M.Move(Direction.Down);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    facing = Direction.Left;
                    M.Move(Direction.Left);
                }
                if (Input.GetKey(KeyCode.D))
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