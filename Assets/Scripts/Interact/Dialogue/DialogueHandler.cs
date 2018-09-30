using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueHandler: MonoBehaviour
{
    #region Fields
    public bool Reading;
    public List<Dialogue> DialogueList;

    private int Startindex;
    private int CurrentIndex;
    #endregion

    // Use this for initialization
    void Start()
    {
        Startindex = 0;
        CurrentIndex = 0;
    }

    #region Next Dialogue
    //Checks if the dialogue is done writing. if so, go to the next dialogue, if not, finish writing
    public void AdvanceDialogue()
    {
        Dialogue d = DialogueList[CurrentIndex];
        if (d.IsWriteDone())
        {
            DestroyDialogueBox();
            if (d.GetType().IsSubclassOf(typeof(Choice)))
            {
                //Start the next dialogue, based on the choice the player made
                Choice c = d as Choice;
                CurrentIndex = c.GetNextIndex();
                SpawnDialogueBox();
                DialogueList[CurrentIndex].StartDialogue();
            }
            else if (d.GetType().IsSubclassOf(typeof(Log)))
            {
                //If enddialogue, end the conversation, else starts the next one
                Log l = d as Log;
                if (l.EndDialogue)
                {
                    EndDialogue();
                }
                else
                {
                    CurrentIndex++;
                    SpawnDialogueBox();
                    DialogueList[CurrentIndex].StartDialogue();
                }
            }
            else
            {
                throw new System.Exception("Unknown dialogue type");
            }
        }
        else
        {
            d.FinishWrite();
        }
    }

    //Updates the cursor if the current dialogue is a choice & the text is done writing
    public void UpdateSelectedChoice(int direction)
    {
        Dialogue d = DialogueList[CurrentIndex];
        if (d.IsWriteDone() && d.GetType().IsSubclassOf(typeof(Choice)))
        {
            Choice c = d as Choice;
            c.UpdateSelectedIndex(direction);
        }
    }
    #endregion

    #region Starting and ending
    //Starts the dialogue
    public void StartReading()
    {
        CurrentIndex = Startindex;
        SpawnDialogueBox();
        DialogueList[CurrentIndex].StartDialogue();
        StartCoroutine(activate());
    }
    IEnumerator activate()
    {
        Reading = true;
        yield return new WaitForEndOfFrame();
        while (Reading)
        {
            yield return null;
        }
    }

    //Ends the dialogue, resets the handler & dialogue
    public void EndDialogue()
    {
        foreach (Dialogue i in DialogueList)
        {
            i.ResetDialogue();
        }

        Reading = false;
    }
    #endregion

    #region Spawning and destroying
    //Spawns the dialogueBox
    void SpawnDialogueBox()
    {
        DialogueList[CurrentIndex].SpawnDialogueBox();
    }

    //Destroys the dialogueBox
    void DestroyDialogueBox()
    {
        DialogueList[CurrentIndex].DestroyDialogueBox();
    }
    #endregion
}
