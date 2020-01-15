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
        LookAt(Global.PlayerMovement);
        base.Interact();
    }

    //Makes the object look at the target
    public void LookAt(Movement target)
    {
        switch (target.Direction)
        {
            case Direction.Up:
                target.Direction = Direction.Down;
                break;
            case Direction.Down:
                target.Direction = Direction.Up;
                break;
            case Direction.Left:
                target.Direction = Direction.Right;
                break;
            case Direction.Right:
                target.Direction = Direction.Left;
                break;
        }
    }
}