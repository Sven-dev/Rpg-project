using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    public List<Action> ActionList;

    private bool active;
    private int currentIndex;

    public virtual void Interact()
    {
        StartCoroutine(interaction());
    }

    //Stops the interactor until all parts if the interaction have been executed
    IEnumerator interaction()
    {
        Global.PlayerMovement.Immobile = true;

        //Starts the first action in the list
        active = true;
        ActionList[0].Play();

        while (active)
        {
            if (ActionList[currentIndex].Active == false)
            {
                nextaction();
            }

            yield return null;
        }

        Global.PlayerMovement.Immobile = false;
    }

    private void nextaction()
    {
        if (currentIndex < ActionList.Count - 1)
        {
            currentIndex++;
            ActionList[currentIndex].Play();
            return;
        }

        active = false;
    }
}