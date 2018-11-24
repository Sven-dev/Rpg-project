using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;
    AttackInteracter IA;
    DialogueHandler DH;
    public bool Active;

    void Start()
    {
        Active = true;
        DontDestroyOnLoad(gameObject);
        M = GetComponent<Movement>();
        IA = transform.GetChild(0).GetComponent<AttackInteracter>();
        DH = GetComponent<DialogueHandler>();
    }

    //Handles the movement (which is influenced by physics)
    private void FixedUpdate()
    {
        if (Active)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (DH.Reading)
                {
                    DH.AdvanceDialogue();
                }
                else
                {
                    #region Attack / Interact
                    IA.CheckForInteract();
                    #endregion
                }
            }        
            else if (Input.GetKey(KeyCode.D))
            {
                if (DH.Reading)
                {
                    DH.UpdateSelectedChoice(1);
                }
                else
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
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (DH.Reading)
                {
                    DH.UpdateSelectedChoice(-1);
                }
                else
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
                #region idle
                if (M.Idle == false)
                    M.Idle = true;
                #endregion
            }
        }
    }
}