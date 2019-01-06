using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : Action
{
    public Dialogue[] Dialogue;

    public override void StartProcess(GameObject target)
    {
        ConversationManager manager = target.GetComponent<ConversationManager>();
        manager.Talk(this);
        StartCoroutine(_Conversation());
    }

    //Waits until the conversation is over
    IEnumerator _Conversation()
    {
        Active = true;
        while (Active)
        {
            yield return null;
        }
    }  
}
