using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;
    AttackInteracter IA;
    ConversationManager CM;
    public bool Active;

    void Start()
    {
        Active = true;
        DontDestroyOnLoad(gameObject);
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
            if (Input.GetKey(KeyCode.D))
            {
                #region moving right
                if (Input.GetKey(KeyCode.W))
                {
                    M.Direction = Direction.UpRight;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    M.Direction = Direction.DownRight;
                }
                else
                {
                    M.Direction = Direction.Right;
                }

                if (M.Idle == true)
                    M.Idle = false;
                M.Move();
                #endregion
            }
            else if (Input.GetKey(KeyCode.A))
            {
                #region moving left
                    if (Input.GetKey(KeyCode.W))
                    {
                        M.Direction = Direction.UpLeft;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        M.Direction = Direction.DownLeft;
                    }
                    else
                    {
                        M.Direction = Direction.Left;
                    }

                    if (M.Idle == true)
                        M.Idle = false;
                    M.Move();
                    #endregion
            }
            else if (Input.GetKey(KeyCode.W))
            {
                #region moving up
                if (Input.GetKey(KeyCode.D))
                {
                    M.Direction = Direction.UpRight;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    M.Direction = Direction.UpLeft;
                }
                else
                {
                    M.Direction = Direction.Up;
                }

                if (M.Idle == true)
                    M.Idle = false;
                M.Move();
                #endregion
            }
            else if (Input.GetKey(KeyCode.S))
            {
                #region moving down
                if (Input.GetKey(KeyCode.D))
                {
                    M.Direction = Direction.DownRight;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    M.Direction = Direction.DownLeft;
                }
                else
                {
                    M.Direction = Direction.Down;
                }

                if (M.Idle == true)
                    M.Idle = false;
                M.Move();
                #endregion
            }
            else
            {
                #region Idle
                if (M.Idle == false)
                {
                    M.Idle = true;
                }
                #endregion
            }
        }
    }
}