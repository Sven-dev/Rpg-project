using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InteractableObject : Interactable
{
    public List<Action> ActionList;

    private bool active;
    private int currentIndex;

    // Use this for initialization
    void Start()
    {
        active = false;
        currentIndex = 0;
    }

    IEnumerator startqueue()
    {
        p.Controls_OFF();

        //Starts the first action in the list
        this.active = true;
        yield return new WaitForEndOfFrame();
        ActionList[0].StartProcess();

        while (active)
        {
            if (ActionList[currentIndex].Active == false)
            {
                nextaction();
            }
            yield return new WaitForEndOfFrame();
        }

        p.Controls_ON();
    }

    public override void Interact()
    {
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
            this.active = false;
        }
    }
}
