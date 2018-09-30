using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Interactable {

    private Movement PlayerMovement;

    Movement Movement;
    public List<Action> ActionList;

    private bool active;
    private int currentIndex;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        PlayerMovement = Player.GetComponent<Movement>();
        Movement = GetComponent<Movement>();
    }

    public override void Interact()
    {
        StartCoroutine(interaction());
    }

    //Stops the interactor until all parts if the interaction have been executed
    IEnumerator interaction()
    {
        PlayerMovement.Immobile = true;
        Movement.LookAt(PlayerMovement);

        //Starts the first action in the list
        active = true;
        ActionList[0].StartProcess(Player);

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

    void nextaction()
    {
        if (currentIndex < ActionList.Count - 1)
        {
            currentIndex++;
            ActionList[currentIndex].StartProcess(Player);
            return;
        }

        active = false;
    }
}