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

/// <summary>
/// Handles movement for a character.
/// </summary>
public class Movement : MonoBehaviour
{   
    /// <summary>
    /// The speed at which the character walks.
    /// </summary>
    public float Speed;

    /// <summary>
    /// The direction the character is facing.
    /// </summary>
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
    private Direction _direction = Direction.Right;
    
    /// <summary>
    /// Wether the character is currently walking.
    /// </summary>
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
    private bool _idle = true;

    /// <summary>
    /// Wether the character is allowed to walk or not.
    /// </summary>
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
    private bool _immobile = false;

    public delegate void DirectionChanged(Direction direction);
    public event DirectionChanged OnDirectionChange;

    public delegate void IdleChanged(bool idle);
    public event IdleChanged OnIdleChange;

    private Renderer Renderer;
    private Animator Animator;

    /// <summary>
    /// Gets the renderer and animator, and sets the sorting layer.
    /// </summary>
    void Start ()
    {
        Renderer = GetComponent<Renderer>();
        Animator = GetComponent<Animator>();

        SetSortingLayer();
    }

    //Makes the player move
    /// <summary>
    /// Move the character.
    /// </summary>
    /// <param name="direction">The direction the character is moving in</param>
    public void Move(Direction direction)
    {
        Vector3 temp = DirectionToVector(direction);
        transform.Translate(temp * Speed * Time.fixedDeltaTime);
        Animator.SetFloat("Horizontal", temp.x);
        Animator.SetFloat("Vertical", temp.y);
        SetSortingLayer();
    }

    /// <summary>
    /// Converts the Enum direction to a Vector. Used for moving around.
    /// </summary>
    /// <param name="facing">The direction the character is facing</param>
    /// <returns></returns>
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

    /// <summary>
    /// Sets the render-order, relative to the other objects in the scene,
    /// making it look like the object walks behind other objects.
    /// </summary>
    void SetSortingLayer()
    {
        float objHeight = transform.position.y - Renderer.bounds.size.y / 2f;
        Renderer.sortingOrder = (int)(objHeight * -100);
    }
}