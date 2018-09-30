using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : Action {

    public List<Dialogue> DialogueList;

    public override void StartProcess(GameObject target)
    {
        DialogueHandler d = target.GetComponent<DialogueHandler>();
        d.DialogueList = DialogueList;
        StartCoroutine(Reading(d));
    }

    IEnumerator Reading(DialogueHandler d)
    {
        Active = true;
        d.StartReading();
        while (d.Reading)
        {
            yield return null;
        }

        Active = false;
    }
}