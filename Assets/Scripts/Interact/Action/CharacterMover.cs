using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveOption
{
    Diagonal,
    X_then_Y,
    Y_then_X
};

public class CharacterMover : Action
{
    //Characterlist
    public List<Movement> Characters;
    public List<LocationData> Data;
	
    public override void Play()
    {
        Active = true;
        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            StartCoroutine(Move(Characters[i], Data[i]));
        }

        Active = true;
        while (!Finished())
        {
            yield return null;
        }

        Active = false;
        yield return null;
        foreach (LocationData data in Data)
        {
            data.Reset();
        }
    }

    IEnumerator Move(Movement character, LocationData data)
    {
        while (data.Active)
        {
            #region X_then_y
            if (data.Option == MoveOption.X_then_Y)
            {
                if (Mathf.Abs(data.Direction.x) > 0.1f)
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
            }
            #endregion
            #region Y_then_X
            else if (data.Option == MoveOption.Y_then_X)
            {
                if (Mathf.Abs(data.Direction.y) > 0.1f)
                {
                    character.VectorToDirection(new Vector2(0, data.Direction.y));
                    character.Idle = false;
                    character.Move(data);
                }
                else if (Mathf.Abs(data.Direction.x) > 0.1f)
                {
                    character.VectorToDirection(new Vector2(data.Direction.x, 0));
                    character.Idle = false;
                    character.Move(data);
                }
                else
                {
                    data.Index++;
                }
            }
            #endregion
            #region Diagonal
            else if (data.Option == MoveOption.Diagonal)
            {
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
            }
            #endregion

            yield return null;
        }

        character.Direction = data.EndDirection;
        character.Idle = true;
    }

    bool Finished()
    {
        foreach (LocationData data in Data)
        {
            if (data.Active)
                return false;
        }

        return true;
    }
}