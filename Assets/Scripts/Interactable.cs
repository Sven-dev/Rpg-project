using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool Selected;
    public List<Action> ActionList;

    private bool active;
    private int currentIndex;
    private SpriteRenderer SelectionMarker;

    private void Awake()
    {
        SelectionMarker = transform.GetChild(0).GetComponent<SpriteRenderer>();
        SelectionMarker.enabled = false;
    }

    //Interacts with the object
    public virtual void Interact()
    {
        StartCoroutine(_interact());
    }

    //Stops the player until all parts if the interaction have been executed
    private IEnumerator _interact()
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

    //Selects the object
    public void Select()
    {
        Selected = true;
        StartCoroutine(_Select());
    }

    public void Deselect()
    {
        Selected = false;
    }

    //Enables the marker as long as the object is selected
    private IEnumerator _Select()
    {
        SelectionMarker.enabled = true;
        while (Selected)
        {
            yield return null;
        }

        SelectionMarker.enabled = false;
    }
}