using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;
    AttackInteracter IA;
    DialogueHandler DH;

    void Start()
    {
        M = GetComponent<Movement>();
        IA = transform.GetChild(0).GetComponent<AttackInteracter>();
        DH = GetComponent<DialogueHandler>();
        StartCoroutine(KeyCheck());
    }

    IEnumerator KeyCheck()
    {
        bool active = true;
        while (active)
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

                M.Idle = false;
                M.Move();
                #endregion
            }
            else
            {
                #region idle
                if (M.Idle == false)
                {
                    M.Idle = true;
                }
                #endregion
            }

            yield return null;
        }
    }
}