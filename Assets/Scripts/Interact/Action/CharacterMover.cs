using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MoveOption
{
    X_then_Y,
    Y_then_X,
    Diagonal,
    Straight
};

public class CharacterMover : Action
{
    public List<Movement> Characters;
    public List<GameObject> Paths;
    [Tooltip("The direction the characters will face after moving")]
    public List<Direction> Facing;

    private List<bool> CompletedPaths;
	
    public override void Play()
    {
        CompletedPaths = new List<bool>();
        StartCoroutine(_Playing());
    }

    IEnumerator _Playing()
    {
        Active = true;
        for (int i = 0; i < Characters.Count; i++)
        {
            CompletedPaths.Add(false);
            List<Waypoint> waypoints = Paths[i].transform.GetComponentsInChildren<Waypoint>().ToList();
            StartCoroutine(_Travel(Characters[i], waypoints, i));
        }

        while(CompletedPaths.Any(c=> c == false))
        {
            yield return null;
        }

        Active = false;
    }

    IEnumerator _Travel(Movement character, List<Waypoint> waypoints, int index)
    {
        character.Idle = false;
        while (waypoints.Count > 0)
        {
            if (waypoints[0].Pattern == MoveOption.X_then_Y)
            {
                MoveXY(character, waypoints[0]);
            }
            else if (waypoints[0].Pattern == MoveOption.Y_then_X)
            {
                MoveYX(character, waypoints[0]);
            }
            else if (waypoints[0].Pattern == MoveOption.Diagonal)
            {
                MoveDiagonal(character, waypoints[0]);
            }
            else if (waypoints[0].Pattern == MoveOption.Straight)
            {
                MoveStraight(character, waypoints[0]);
            }

            if (character.transform.position == waypoints[0].transform.position)
            {
                waypoints.RemoveAt(0);
            }

            yield return null;
        }

        character.Direction = Facing[index];
        CompletedPaths[index] = true;
        character.Idle = true;
    }

    private void MoveXY(Movement character, Waypoint target)
    {
        //if the player does not have the same x-coordinate as the target
        if (character.transform.position.x != target.transform.position.x)
        {
            //move the character towards target
            character.Move(new Vector2(target.transform.position.x - character.transform.position.x, 0).normalized);

            //if the character is close enough to the target
            if (Mathf.Abs(character.transform.position.x - target.transform.position.x) < 0.01f)
            {
                //set the position of the character to the target
                character.transform.position = new Vector2(target.transform.position.x, character.transform.position.y);
                return;
            }
        }
        //if the player does not have the same x-coordinate as the target
        else if (character.transform.position.y != target.transform.position.y)
        {
            //move the character towards target
            character.Move(new Vector2(0, target.transform.position.y - character.transform.position.y).normalized);

            //if the character is close enough to the target
            if (Mathf.Abs(character.transform.position.y - target.transform.position.y) < 0.01f)
            {
                //set the position of the character to the target
                character.transform.position = new Vector2(character.transform.position.x, target.transform.position.y);
                return;
            }
        }
    }

    private void MoveYX(Movement character, Waypoint target)
    {
        //if the player does not have the same x-coordinate as the target
        if (character.transform.position.y != target.transform.position.y)
        {
            //move the character towards target
            character.Move(new Vector2(0, target.transform.position.y - character.transform.position.y).normalized);

            //if the character is close enough to the target
            if (Mathf.Abs(character.transform.position.y - target.transform.position.y) < 0.01f)
            {
                //set the position of the character to the target
                character.transform.position = new Vector2(character.transform.position.x, target.transform.position.y);
                return;
            }
        }
        //if the player does not have the same x-coordinate as the target
        else if (character.transform.position.x != target.transform.position.x)
        {
            //move the character towards target
            character.Move(new Vector2(target.transform.position.x - character.transform.position.x, 0).normalized);

            //if the character is close enough to the target
            if (Mathf.Abs(character.transform.position.x - target.transform.position.x) < 0.01f)
            {
                //set the position of the character to the target
                character.transform.position = new Vector2(target.transform.position.x, character.transform.position.y);
                return;
            }
        }
    }

    private void MoveDiagonal(Movement character, Waypoint target)
    {
        character.Move(target.transform.position);
        if (Vector2.Distance(character.transform.position, target.transform.position) < 1)
        {
            character.transform.position = target.transform.position;
        }
        /*
        if (Mathf.Abs(data.Direction.x) > 0.1f && Mathf.Abs(data.Direction.y) > 0.1f)
        {
            character.VectorToDirection(data.Direction);
            character.Idle = false;
            character.Move(data);
        }
        else if (Mathf.Abs(data.Direction.x) > 0.1f)
        {
            character.VectorToDirection(new Vector2(data.Direction.x, 0));
            character.Idle = false;
            character.Move(data);
        }
        else if (Mathf.Abs(data.Direction.y) > 0.1f)
        {
            character.VectorToDirection(new Vector2(0, data.Direction.y));
            character.Idle = false;
            character.Move(data);
        }
        else
        {
            data.Index++;
        }
        */
    }

    private void MoveStraight(Movement character, Waypoint target)
    {
        character.Move(target.transform.position);
        if (Vector2.Distance(character.transform.position, target.transform.position) < 0.01f)
        {
            character.transform.position = target.transform.position;
        }
    }
}