using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : Interactable
{
    public DialogueHandler dialogue;

    public override void Interact()
    {
        StartCoroutine(interaction());
    }

    IEnumerator interaction()
    {
        //dialogue.StartReading();

        while (dialogue.Reading)
        {
            yield return new WaitForEndOfFrame();
        }

        Player p = GameObject.FindWithTag("Player").GetComponent<Player>();
        //p.Controls_ON();
    }
}
