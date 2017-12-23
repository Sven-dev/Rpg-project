using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class Choice : Dialogue {

    #region Fields
    public List<string> ChoiceList;
    
    public List<int> ChoiceIndexList;

    protected int SelectedChoice;
    #endregion

    #region UnityFields
    public GameObject CursorPrefab;
    protected GameObject CursorClone;
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


        Text CTA = DialogueBoxClone.transform.Find("Canvas/Choice/A").GetComponent<Text>();

        if (CTA != null)
        {
            CTA.text = ChoiceList[0];
        }

        Text CTB = DialogueBoxClone.transform.Find("Canvas/Choice/B").GetComponent<Text>();

        if (CTB != null)
        {
            CTB.text = ChoiceList[1];
        }

        CTB.transform.GetChild(0).GetComponent<Image>().color = Color.white;

        CursorClone.transform.parent = DialogueBoxClone.transform;
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
        Transform TF;
        Vector3 POS;

        if (SelectedChoice == 0)
        {
            TF = DialogueBoxClone.transform.Find("Canvas/ChoiceA");
        }
        else //(SelectedChoice == 1)
        {
            TF = DialogueBoxClone.transform.Find("Canvas/ChoiceB");
        }

        POS = TF.transform.position;
        POS.x -= 0.23f * ChoiceList[SelectedChoice].Length / 2 + 0.6f;
        POS.y += 0.13f;

        CursorClone.transform.position = POS;
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
    }
    #endregion
}
