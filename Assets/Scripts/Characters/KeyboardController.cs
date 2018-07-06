using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    Movement M;

    void Start()
    {
        M = GetComponent<Movement>();
    }

    void Update()
    {
        #region moving right
        if (Input.GetKey(KeyCode.D))
        {
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
        }
        #endregion
        #region moving left
        else if (Input.GetKey(KeyCode.A))
        {
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
        }
        #endregion
        #region moving up
        else if (Input.GetKey(KeyCode.W))
        {
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
        }
        #endregion
        #region moving down
        else if (Input.GetKey(KeyCode.S))
        {
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
        }
        #endregion
        #region idle
        else
        {
            if (M.Idle == false)
            M.Idle = true;
        }
        #endregion
    }
}