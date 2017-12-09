using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueHandler: Action
{
    #region Fields
    public List<Dialogue> DialogueList;

    private int Startindex;
    private int Currentindex;
    #endregion

    // Use this for initialization
    void Start()
    {
        Startindex = 0;
        Currentindex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            CheckKeys();
        }
    }

    #region KeyChecks
    //Checks if the current dialogue is a log or a choice, and calls the correct CheckKey method
    void CheckKeys()
    {
        if (Active)
        {
            Dialogue currentdialogue = DialogueList[Currentindex];

            //If currentdialogue inherits from Choice
            if (currentdialogue.GetType().IsSubclassOf(typeof(Choice)))
            {
                CheckKeys(currentdialogue as Choice);
            }
            else //If currentdialogue inherits from Log
            {
                CheckKeys(currentdialogue as Log);
            }
        }
    }

    //Checks the keyinputs of the current dialogue, if it is a log
    void CheckKeys(Log currentdialogue)
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            //If dialogue has ended
            if (currentdialogue.IsWriteDone())
            {
                NextDialogueLog(currentdialogue);
            }
            //If dialogue has not ended
            else
            {
                currentdialogue.FinishWrite();
            }
        }
        else
        {
            if (currentdialogue.IsWriteDone())
            {
                if (currentdialogue.Autonext)
                {
                    NextDialogueLog(currentdialogue);
                }
            }
        }
    }

    //Checks the keyinputs of the current dialogue, if it is a choice
    void CheckKeys(Choice currentdialogue)
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (currentdialogue.IsWriteDone())
            {
                NextDialogueChoice(currentdialogue);
            }
            else
            {
                currentdialogue.FinishWrite();
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentdialogue.UpdateSelectedIndex(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentdialogue.UpdateSelectedIndex(1);
        }
    }
    #endregion

    #region Next Dialogue
    //Checks if the dialogue ends, and if not, selects the next dialogue
    void NextDialogueLog(Log currentdialogue)
    {
        DestroyDialogueBox();

        if (currentdialogue.EndDialogue)
        {
            EndDialogue(currentdialogue.EndResult);
        }
        else
        {
            Currentindex = currentdialogue.NextIndex;
            SpawnDialogueBox();
            DialogueList[Currentindex].StartDialogue();
        }
    }

    //Destroyes the current dialoguebox, and spawns the right next dialoguebox
    void NextDialogueChoice(Choice currentdialogue)
    {
        DestroyDialogueBox();

        Currentindex = currentdialogue.GetNextIndex();
        SpawnDialogueBox();
        DialogueList[Currentindex].StartDialogue();
    }
    #endregion

    #region Starting and ending
    //Starts the dialogue
    public override void StartProcess()
    {
        Currentindex = Startindex;
        SpawnDialogueBox();
        DialogueList[Currentindex].StartDialogue();

        Active = true;
    }

    //Ends the dialogue, resets the handler, and unlocks player controls
    //OLD: Returns the result of the dialogue, in the form of an integer
    //triggersystem should solve this, not dialoguehandlers
    public void EndDialogue(int endresult)
    {
        ResetDialogue();

        Active = false;

        //TO DO: is supposed to return a positive or negative result of the dialogue
        //return 0;
    }

    //Resets the dialoguehandler and all its dialogue
    void ResetDialogue()
    {
        foreach (Dialogue i in DialogueList)
        {
            i.ResetDialogue();
        }

        Active = false;
    }
    #endregion

    #region Spawning and destroying
    //Spawns the dialogueBox
    void SpawnDialogueBox()
    {
        DialogueList[Currentindex].SpawnDialogueBox();
    }

    //Destroys the dialogueBox
    void DestroyDialogueBox()
    {
        DialogueList[Currentindex].DestroyDialogueBox();
    }
    #endregion
}
