using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : Action
{
    public Dialogue[] Dialogue;
    private ConversationManager Player;

    private void Start()
    {
        Player = GlobalVariables.Player.GetComponent<ConversationManager>();
    }

    public override void Play()
    {
        Player.Talk(this);
        StartCoroutine(_Playing());
    }

    //Waits until the conversation is over
    IEnumerator _Playing()
    {
        Active = true;
        while (Active)
        {
            yield return null;
        }
    }
}
