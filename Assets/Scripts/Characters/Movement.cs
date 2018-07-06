﻿using System.Collections;
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

public class Movement : MonoBehaviour {

    public delegate void MovementChange();
    public event MovementChange OnAnimationChange;

    public Direction _direction;
    public bool _idle;

    public float Speed;
    private Player P;
    private Renderer R;

    public Direction Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
            if (OnAnimationChange != null)
                OnAnimationChange();
        }
    }

    public bool Idle
    {
        get { return _idle; }
        set
        {
            _idle = value;
            if (OnAnimationChange != null)
                OnAnimationChange();
        }
    }

    // Use this for initialization
    void Start ()
    {
        P = GetComponent<Player>();
        R = GetComponent<Renderer>();

        Idle = true;
        Direction = Direction.Right;
        SetSortingLayer();
    }

    //Makes the player move
    public void Move()
    {
        transform.position += DirectionToVector() * Speed * Time.fixedDeltaTime;
        SetSortingLayer();
    }

    //Makes the object move towards a location
    public void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);
        SetSortingLayer();
    }

    //Converts the Enum direction to a Vector. Used for moving around
    Vector3 DirectionToVector()
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

    //Sets the objects sorting layer to be the same as the objects y-coördinate, allowing walking behind other objects
    void SetSortingLayer()
    {
        float objHeight = R.bounds.size.y * 0.8f;
        R.sortingOrder = (int)((transform.position.y - objHeight) * -10);
    }

    public int GetSortingLayer()
    {
        float objHeight = R.bounds.size.y * 0.8f;
        return (int)((transform.position.y - objHeight) * -10);
    }
}