using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            List<Transform> waypoints = Paths[i].transform.Cast<Transform>().ToList();
            StartCoroutine(_Travel(Characters[i], waypoints, i));
        }

        while(CompletedPaths.Any(c=> c == false))
        {
            yield return null;
        }

        Active = false;
    }

    IEnumerator _Travel(Movement character, List<Transform> waypoints, int index)
    {
        character.Idle = false;
        while (waypoints.Count > 0)
        {
            MoveTowardsTarget(character, waypoints[0]);
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

    private void MoveTowardsTarget(Movement character, Transform target)
    {
        Vector2 direction = target.transform.position - character.transform.position;
        character.Move(direction.normalized);
        if (Vector2.Distance(character.transform.position, target.transform.position) < 0.01f)
        {
            character.transform.position = target.transform.position;
        }
    }
}