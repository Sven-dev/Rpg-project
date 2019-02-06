using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : Trigger
{
    public List<Action> ActionList;
    public bool DisableWhenFinished;
    private bool Active;
    private int CurrentIndex;
    private Movement PlayerMovement;

    // Use this for initialization
    private void Start()
    {
        Active = false;
        CurrentIndex = 0;
    }

    protected override void ExecuteTrigger()
    {
        StartCoroutine(startqueue());
    }

    //Stops the executer until all parts of the cutscene have been executed
    IEnumerator startqueue()
    {
        PlayerMovement.Immobile = true;

        //Starts the first action in the list
        Active = true;
        ActionList[CurrentIndex].Play();

        while (Active)
        {
            if (ActionList[CurrentIndex].Active == false)
            {
                nextaction();
            }
            yield return new WaitForEndOfFrame();
        }

        PlayerMovement.Immobile = false;
    }

    //Starts the next action in the list. If there's no actions left, ends the cutscene
    void nextaction()
    {
        if (CurrentIndex < ActionList.Count - 1)
        {
            CurrentIndex++;
            ActionList[CurrentIndex].Play();
        }
        else
        {
            if(DisableWhenFinished)
            {
                GetComponent<Collider2D>().enabled = false;
            }

            CurrentIndex = 0;
            Active = false;
        }
    }
}