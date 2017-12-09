using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : Interactable
{
    public DialogueHandler dialogue;

    public override void Interact()
    {
        Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
        p.Controls_OFF();

        if (dialogue != null)
        {
            dialogue.StartProcess();
        }
        else
        {
            p.Controls_ON();
        }
    }
}
