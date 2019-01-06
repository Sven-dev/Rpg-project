using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConversationManager : MonoBehaviour
{
    public bool Active;

    private DialogueManager DialogueManager;
    private ChoiceManager ChoiceManager;
    private PortraitDialogueManager PortraitDialogueManager;
    private PortraitChoiceManager PortraitChoiceManager;

    private Conversation Conversation;
    private List<Dialogue> Dialogue;
    private int index;

    private void Start()
    {
        DialogueManager = Camera.main.GetComponentInChildren<DialogueManager>(true);
        ChoiceManager = Camera.main.GetComponentInChildren<ChoiceManager>(true);
        PortraitDialogueManager = Camera.main.GetComponentInChildren<PortraitDialogueManager>(true);
        PortraitChoiceManager = Camera.main.GetComponentInChildren<PortraitChoiceManager>(true);
    }

    public void Talk(Conversation conversation)
    {
        Conversation = conversation;
        Dialogue = conversation.Dialogue.ToList();
        StartCoroutine(_Talk());
    }

    //Plays through dialogue[]
    IEnumerator _Talk()
    {
        index = 0;
        Active = true;
        while (Active)
        {
            //Start a new dialogue
            StartDialogue(Dialogue[index]);

            //Wait while the dialogue is being played
            while(Dialogue[index].Active)
            {
                yield return null;
            }

            //if the dialogue is a choice, add the results of the choice to the list
            if (Dialogue[index] is Choice || Dialogue[index] is PortraitChoice)
            {
                AddResult(Dialogue[index]);
            }

            //if all dialogue is read, end
            index++;
            if (index >= Dialogue.Count)
            {
                Active = false;
            }
        }

        Conversation.Active = false;
    }

    //Gets the correct dialogue, and starts one of the DialogueManagers based on what type it is.
    public void StartDialogue(Dialogue d)
    {
        if (d is Log)
        {
            DialogueManager.StartDialogue(d);
        }
        else if (d is Choice)
        {
            ChoiceManager.StartDialogue(d);
        }
        else if (d is PortraitLog)
        {
            PortraitDialogueManager.StartDialogue(d);
        }
        else if (d is PortraitChoice)
        {
            PortraitChoiceManager.StartDialogue(d);
        }
    }

    //Auto-completes the dialogue. if it's already completed, start the next one.
    public void FinishTalking()
    {
        if(Dialogue[index].Writing)
        {
            Dialogue[index].Writing = false;
        }
        else
        {
            Dialogue[index].Active = false;
        }
    }

    //Casts the Dialogue object to the correct Choice object and adds new dialogue to the list based on the choice made
    private void AddResult(Dialogue d)
    {
        int result = -1;
        if (Dialogue[index] is Choice)
        {
            result = ChoiceManager.SelectedIndex();
        }
        else if (Dialogue[index] is PortraitChoice)
        {
            result = PortraitChoiceManager.SelectedIndex();
        }

        Dialogue.AddRange(Dialogue[index].transform.GetChild(result).GetComponents<Dialogue>());
    }

    //Moves the cursor of the choice objects
    public void MoveCursor(int direction)
    {
        if (Dialogue[index] is Choice)
        {
            ChoiceManager.ChangeIndex(direction);
        }
        else if (Dialogue[index] is PortraitChoice)
        {
            PortraitChoiceManager.ChangeIndex(direction);
        }
    }
}