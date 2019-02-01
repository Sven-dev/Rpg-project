using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : MonoBehaviour
{
    private List<Vector2> DirectionsBackup;

    public List<Vector2> Directions;
    public List<MoveOption> Options;
    public Direction EndDirection;

    [HideInInspector]
    public bool Active;
    private int _index;

    public Vector3 Direction
    {
        get { return Directions[Index]; }
        set
        {
            Directions[Index] = value;
            if (Mathf.Abs(Directions[Index].x) <= 0.1f && Mathf.Abs(Directions[Index].y) <= 0.1f)
            {
                Index++;
            }
        }
    }

    public MoveOption Option
    {
        get { return Options[Index]; }
    }

    //keeps track of the index. if the index is done
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;            
            if (_index >= Directions.Count)
            {
                value = Directions.Count;
                Active = false;
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
        Index = 0;
        Active = true;
        BackUp();
	}

    public void Reset()
    {
        Index = 0;
        Active = true;
        Restore();
    }

    void BackUp()
    {
        DirectionsBackup = new List<Vector2>();
        foreach (Vector2 d in Directions)
        {
            DirectionsBackup.Add(new Vector2(d.x, d.y));
        }
    }

    void Restore()
    {
        Directions = new List<Vector2>();
        foreach (Vector2 d in DirectionsBackup)
        {
            Directions.Add(new Vector2(d.x, d.y));
        }
    }
}