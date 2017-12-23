using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Choice : Dialogue {

    #region Fields
    public List<string> ChoiceList;
    
    public List<int> ChoiceIndexList;

    protected int SelectedChoice;

    private Text A;
    private Text B;
    private Image ACursor;
    private Image BCursor;
    #endregion

    #region Writing
    //Checks the progress of the textwriting, returns true if finished
    public override bool IsWriteDone()
    {
        if (base.IsWriteDone())
        {
            SpawnChoice();
            return true;
        }

        return false;
    }

    //Checks the progress of the textwriting, returns true if finished
    public override void FinishWrite()
    {
        base.FinishWrite();
        IsWriteDone();
    }
    #endregion

    #region Choice logic
    //Spawns the choice, and the cursor, so the player can choose
    protected virtual void SpawnChoice()
    {
        //Add data to option A
        A = DialogueBoxClone.transform.Find("Canvas/Choice/A").GetComponent<Text>();
        A.text = ChoiceList[0];
        ACursor = A.transform.Find("Cursor").GetComponent<Image>();

        //Add data to option B
        B = DialogueBoxClone.transform.Find("Canvas/Choice/B").GetComponent<Text>();
        B.text = ChoiceList[1];
        BCursor = B.transform.Find("Cursor").GetComponent<Image>();
   
        UpdateCursor();
    }

    //Updates the selected option, given it falls inside the list
    public virtual void UpdateSelectedIndex(int amount)
    {
        if (SelectedChoice + amount >= 0 && SelectedChoice + amount <= ChoiceList.Count -1)
        {
            SelectedChoice += amount;
            UpdateCursor();
        }
    }

    //Updates the position of the cursor
    protected virtual void UpdateCursor()
    {
        //Change the alpha value of the selected choice to 1 
        //and the other to 0 (makes the option in visible)
        if (SelectedChoice == 0)
        {
            ACursor.color = new Color(1, 1, 1, 1);
            BCursor.color = new Color(1, 1, 1, 0);
        }
        else //(SelectedChoice == 1)
        {
            ACursor.color = new Color(1, 1, 1, 0);
            BCursor.color = new Color(1, 1, 1, 1);
        }
    }

    //Returns the index of the selected choice.
    public int GetNextIndex()
    {
        return ChoiceIndexList[SelectedChoice];
    }

    //Returns the ending int of the selected choice.
    #endregion

    #region Resetting
    public override void ResetDialogue()
    {
        base.ResetDialogue();

        SelectedChoice = 0;
        A.text = "";
        B.text = "";

        ACursor.color = new Color(1, 1, 1, 0);
        BCursor.color = new Color(1, 1, 1, 0);
    }
    #endregion
}
