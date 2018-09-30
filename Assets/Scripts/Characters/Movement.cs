using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public class Movement : MonoBehaviour
{
    Renderer R;

    public float Speed;
    Direction _direction;
    public bool _idle;
    public bool _immobile;

    public delegate void MovementChanged();
    public event MovementChanged OnMovementChange;

    public Direction Direction
    {
        get { return _direction; }
        set
        {
            if (!Immobile)
            {
                if (_direction != value)
                {
                    _direction = value;
                    if (OnMovementChange != null)
                        OnMovementChange();
                }
            }
        }
    }

    public bool Idle
    {
        get { return _idle; }
        set
        {
            if (!Immobile)
            {
                _idle = value;
                if (OnMovementChange != null)
                    OnMovementChange();
            }
        }
    }

    public bool Immobile
    {
        get { return _immobile; }
        set
        {
            if (value == true)
                Idle = true;
            _immobile = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        R = GetComponent<Renderer>();

        Immobile = false;
        Idle = true;
        Direction = Direction.Right;
        SetSortingLayer();
    }

    //Makes the player move
    public void Move()
    {
        if (!Immobile)
        {
            transform.position += DirectionToVector() * Speed * Time.deltaTime;
            SetSortingLayer();
        }
    }

    public void Move(LocationData data)
    {
        if (!Immobile)
        {
            transform.position += DirectionToVector() * Speed * Time.deltaTime;
            data.Direction -= DirectionToVector() * Speed * Time.deltaTime;
            SetSortingLayer();
        }
    }

    //Makes the object move towards a location
    public void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);
        SetSortingLayer();
    }

    //Converts the Enum direction to a Vector. Used for moving around
    public Vector3 DirectionToVector()
    {
        Vector3 direction = Vector2.zero;
        switch(Direction)
        {
            case Direction.Up:
                direction = Vector2.up;
                break;
            case Direction.Down:
                direction = Vector2.down;
                break;
            case Direction.Left:
                direction = Vector2.left;
                break;
            case Direction.Right:
                direction = Vector2.right;
                break;
            case Direction.UpLeft:
                direction = Vector2.up + Vector2.left;
                break;
            case Direction.UpRight:
                direction = Vector2.up + Vector2.right;
                break;
            case Direction.DownLeft:
                direction = Vector2.down + Vector2.left;
                break;
            case Direction.DownRight:
                direction = Vector2.down + Vector2.right;
                break;
        }

        return direction;
    }

    //Makes the object turn the opposite direction of the target,
    //making it look like the objects are turned to eachother
    public void LookAt(Movement target)
    {
        switch (target.Direction)
        {
            case Direction.Up:
                Direction = Direction.Down;
                break;
            case Direction.Down:
                Direction = Direction.Up;
                break;
            case Direction.Left:
                Direction = Direction.Right;
                break;
            case Direction.Right:
                Direction = Direction.Left;
                break;
            case Direction.UpLeft:
                Direction = Direction.DownRight;
                break;
            case Direction.UpRight:
                Direction = Direction.DownLeft;
                break;
            case Direction.DownLeft:
                Direction = Direction.UpRight;
                break;
            case Direction.DownRight:
                Direction = Direction.UpLeft;
                break;
        }
    }

    //Converts the target vector into a direction
    public void VectorToDirection(Vector2 target)
    {
        Vector2 xnormal = new Vector2(target.x, 0).normalized;
        Vector2 ynormal = new Vector2(0, target.y).normalized;
        Vector2 normal = xnormal + ynormal;

        if (normal.x > 0)
        {
            if (normal.y > 0)
            {
                Direction =  Direction.UpRight;
                return;
            }

            if (normal.y < 0)
            {
                Direction = Direction.DownRight;
                return;
            }

            Direction = Direction.Right;
            return;
        }
        if (normal.x < 0)
        {
            if (normal.y > 0)
            {
                Direction = Direction.UpLeft;
                return;
            }

            if (normal.y < 0)
            {
                Direction = Direction.DownLeft;
                return;
            }

            Direction = Direction.Left;
            return;
        }

        if (normal.y > 0)
        {
            Direction = Direction.Up;
            return;
        }

        if (normal.y < 0)
        {
            Direction = Direction.Down;
            return;
        }
    }

    //sets the render-order, relative to the other objects in the scene,
    //making to look like the object can walk behind other objects
    void SetSortingLayer()
    {
        float objHeight = R.bounds.size.y * 0.8f;
        R.sortingOrder = (int)((transform.position.y - objHeight) * -10);
    }

    //sets the render-order, relative to the other objects in the scene,
    //making to look like the object can walk behind other objects
    public int GetSortingLayer()
    {
        float objHeight = R.bounds.size.y * 0.8f;
        return (int)((transform.position.y - objHeight) * -10);
    }
}