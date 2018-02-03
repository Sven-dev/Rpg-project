using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : Trigger {

    public List<Action> ActionList;
    public bool DisableWhenFinished;
    private bool active;
    private int currentIndex;

	// Use this for initialization
	void Start () {
        active = false;
        currentIndex = 0;
	}
	
    IEnumerator startqueue()
    {        
        //Starts the first action in the list
        this.active = true;
        ActionList[0].StartProcess();

        while (active)
        {
            if (ActionList[currentIndex].Active == false)
            {
                nextaction();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public override void ExecuteTrigger()
    {
        p.Controls_OFF();
        StartCoroutine(startqueue());
    }

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

            this.active = false;

            Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
            p.Controls_ON();
        }
    }
}
