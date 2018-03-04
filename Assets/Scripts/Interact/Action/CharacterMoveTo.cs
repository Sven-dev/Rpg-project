using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveTo : Action
{
    //Characterlist
    public List<Movement> CharacterList;

    public List<GameObject> MoveLocationList;
    public List<MoveOptions> MoveOptionList;
    public List<float> EndDistanceList;

    private List<bool> ActiveList;

    // Use this for initialization
    void Start()
    {
        ActiveList = new List<bool>();

        for (int i = 0; i < EndDistanceList.Count; i++)
        {
            if (EndDistanceList[i] < 1.5f)
            {
                EndDistanceList[i] = 1.5f;
            }

            ActiveList.Add(true);
        }
    }

    public override void StartProcess()
    {
        Active = true;
        StartCoroutine(MoveCharacters());
    }

    IEnumerator MoveCharacters()
    {
        while (Active)
        {
            for (int i = 0; i < CharacterList.Count; i++)
            {
                if (ActiveList[i])
                {
                    CheckMove(i);
                }
            }

            EveryoneMoved();
            yield return new WaitForEndOfFrame();
        }
    }

    void CheckMove(int index)
    {
        float dist = Vector3.Distance(
            CharacterList[index].transform.position, 
            MoveLocationList[index].transform.position);

        if (dist >= 1.5f)
        {
            if (MoveOptionList[index] == MoveOptions.Diagonal)
            {
                CharacterList[index].MoveTo(MoveLocationList[index].transform.position);
            }

            if (MoveOptionList[index] == MoveOptions.X_then_Y)
            {
                if (CharacterList[index].transform.position.x != MoveLocationList[index].transform.position.x)
                {
                    MoveX(index);
                }
                else
                {
                    MoveY(index);
                }
            }

            if (MoveOptionList[index] == MoveOptions.Y_then_X)
            {
                if (CharacterList[index].transform.position.y != MoveLocationList[index].transform.position.y)
                {
                    MoveY(index);
                }
                else
                {
                    MoveX(index);
                }
            }
        }
        else
        {
            ActiveList[index] = false;
        }
    }

    void MoveX(int index)
    {
        CharacterList[index].MoveTo(
            new Vector2(
                MoveLocationList[index].transform.position.x,
                CharacterList[index].transform.position.y));
    }

    void MoveY(int index)
    {
        CharacterList[index].MoveTo(
            new Vector2(
                CharacterList[index].transform.position.x,
                MoveLocationList[index].transform.position.y));
    }

    void EveryoneMoved()
    {
        bool FinalState = true;

        foreach (bool state in ActiveList)
        {
            if (state == true)
            {
                FinalState = false;
            }
        }

        if (FinalState == true)
        {
            Active = false;
        }
    }
}
