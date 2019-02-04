using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : Interactable
{
    private Movement NPCMovement;

    // Use this for initialization
    private void Start()
    {
        NPCMovement = GetComponent<Movement>();
    }

    public override void Interact()
    {
        NPCMovement.LookAt(Global.PlayerMovement);
        base.Interact();
    }
}