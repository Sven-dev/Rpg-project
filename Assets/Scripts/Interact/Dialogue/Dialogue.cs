using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class Dialogue : MonoBehaviour {

    #region Fields
    protected GameObject DialogueBoxClone;

    public string Text;
    private float TextSpeed;

    private float timeStamp;
    private int CurrentLetter;

    private bool Active;
    protected Text DialogueText;
    #endregion

    // Use this for initialization
    void Start ()
    {
        Active = false;

        timeStamp = Time.time;
        TextSpeed = 1f;
	}

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (!IsWriteDone())
            {              
                Write();
            }
        }
    }

    #region Writing
    //Checks the progress of the textwriting, returns true if finished
    public virtual bool IsWriteDone()
    {
        if (CurrentLetter == Text.Length+1)
        {
            Active = false;
            return true;
        }

        return false;
    }

    //Adds a letter to the textbox, if the textspeed is reached.
    private void Write()
    {
        //Debug.Log("Before loop: " + CurrentLetter);
        if (Time.time > timeStamp + TextSpeed * Time.fixedDeltaTime)
        {
            DialogueText.text = Text.Substring(0, CurrentLetter);
            CurrentLetter++;
            timeStamp = Time.time;
        }
    }

    //Finishes the sentence.
    public virtual void FinishWrite()
    {
        CurrentLetter = Text.Length;
    }
    #endregion

    #region Starting and resetting
    //Sets the status of the dialogue to true, starting the dialogue.
    public void StartDialogue()
    {
        Active = true;
    }

    //sets the dialogue back to its original state, allowing it to be used again.
    public virtual void ResetDialogue()
    {
        Active = false;
        DialogueText.text = "";
        CurrentLetter = 0;
    }
    #endregion

    #region Spawning and destroying
    //Spawns the proper dialoguebox on the position of the camera
    public virtual void SpawnDialogueBox()
    {
        GameObject cam = GameObject.Find("Main Camera");

        if (this is CharLog)
        {
            DialogueBoxClone = cam.transform.GetChild(0).gameObject;
        }
        else if (this is CharChoice)
        {
            DialogueBoxClone = cam.transform.GetChild(1).gameObject;
        }
        else if (this is DescLog)
        {
            DialogueBoxClone = cam.transform.GetChild(2).gameObject;
        }
        else if (this is DescChoice)
        {
            DialogueBoxClone = cam.transform.GetChild(3).gameObject;
        }
        else
        {
            throw new System.NotImplementedException("Dialogue box was not defined!");
        }

        DialogueBoxClone.transform.GetChild(0).gameObject.SetActive(true);

        //Gets the text component, and assigns it to a variable
        Text TB = DialogueBoxClone.transform.Find("Canvas/Text").GetComponent<Text>();

        if (TB != null)
        {
            DialogueText = TB;
        }
    }

    //Destroys the dialoguebox when the dialogue is done
    public virtual void DestroyDialogueBox()
    {
        DialogueBoxClone.transform.GetChild(0).gameObject.SetActive(false);
    }
    #endregion
}
