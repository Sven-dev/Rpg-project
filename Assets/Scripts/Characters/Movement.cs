using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right = 100,
    Down = 33,
    Left = 66,
    Up = 0,
    Null = -1
}

public class Movement : MonoBehaviour
{
    private Renderer R;

    private bool _immobile;
    public float Speed;
    private Direction _direction;
    private bool _idle;

    public delegate void DirectionChanged(Direction direction);
    public event DirectionChanged OnDirectionChange;

    public delegate void IdleChanged(bool idle);
    public event IdleChanged OnIdleChange;

    public Animator anim;

    public Direction Direction
    {
        get { return _direction; }
        set
        {
            if (!Immobile && _direction != value)
            {
                _direction = value;
                if (OnDirectionChange != null)
                    OnDirectionChange(value);
            }
        }
    }

    public bool Idle
    {
        get { return _idle; }
        set
        {
            if (!Immobile && _idle != value)
            {
                _idle = value;
                if (OnIdleChange != null)
                    OnIdleChange(value);
            }
        }
    }

    public bool Immobile
    {
        get { return _immobile; }
        set
        {
            _immobile = value;
            if (Immobile)
            {
                _idle = true;
                if (OnIdleChange != null)
                    OnIdleChange(value);
            }
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
    public void Move(Direction direction)
    {
        Vector3 temp = DirectionToVector(direction);
        transform.Translate(temp * Speed * Time.fixedDeltaTime);
        anim.SetFloat("Horizontal", temp.x);
        anim.SetFloat("Vertical", temp.y);
        SetSortingLayer();
    }

    //Makes the object move towards a location
    public void Move(Vector2 direction)
    {
        transform.Translate(direction * Speed * Time.fixedDeltaTime);
        VectorToDirection(direction);
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
        }

        return direction;
    }

    //Converts the Enum direction to a Vector. Used for moving around
    public Vector3 DirectionToVector(Direction facing)
    {
        Vector3 direction = Vector2.zero;
        switch (facing)
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
        }

        return direction;
    }

    //Makes the object look at the target
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
        }
    }

    //Converts the target vector into a direction
    public void VectorToDirection(Vector2 target)
    {
        if (target.y > 0)
        {
            if (target.x > 0.25f && target.x < 0.75f)
            {
                Direction = Direction.Right;
            }
            else if (target.x < -0.33f && target.x > -0.75f)
            {
                Direction = Direction.Left;
            }
            else
            {
                Direction = Direction.Up;
            }
        }
        else if (target.y < 0)
        {
            if (target.x > 0.25f && target.x < 0.75f)
            {
                Direction = Direction.Right;
            }
            else if (target.x < -0.25f && target.x > -0.75f)
            {
                Direction = Direction.Left;
            }
            else
            {
                Direction = Direction.Down;
            }
        }
        else if (target.x > 0)
        {
            Direction = Direction.Right;         
        }
        else if (target.x < 0)
        {
            Direction = Direction.Left;
        }
    }

    //Sets the render-order, relative to the other objects in the scene,
    //making it look like the object walks behind other objects
    void SetSortingLayer()
    {
        float objHeight = transform.position.y - R.bounds.size.y / 2f;
        R.sortingOrder = (int)(objHeight * -100);
    }
}