using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : Trigger {

    public List<Action> ActionList;
    public bool DisableWhenFinished;
    private bool active;
    private int currentIndex;
    private Movement PlayerMovement;

	// Use this for initialization
	new void Start()
    {
        base.Start();
        active = false;
        currentIndex = 0;
        PlayerMovement = Player.GetComponent<Movement>();
    }
	
    //Stops the executer until all parts of the cutscene have been executed
    IEnumerator startqueue()
    {
        PlayerMovement.Immobile = true;

        //Starts the first action in the list
        active = true;
        ActionList[currentIndex].StartProcess(Player);

        while (active)
        {
            if (ActionList[currentIndex].Active == false)
            {
                nextaction();
            }
            yield return new WaitForEndOfFrame();
        }

        PlayerMovement.Immobile = false;
    }

    public override void ExecuteTrigger()
    {
        StartCoroutine(startqueue());
    }

    //Starts the next action in the list. If there's no actions left, ends the cutscene
    void nextaction()
    {
        if (currentIndex < ActionList.Count - 1)
        {
            currentIndex++;
            ActionList[currentIndex].StartProcess();
        }
        else
        {
            if(DisableWhenFinished)
            {
                GetComponent<BoxCollider>().enabled = false;
            }

            currentIndex = 0;
            active = false;
        }
    }
}
