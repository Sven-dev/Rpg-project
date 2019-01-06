using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager: MonoBehaviour
{
    [HideInInspector]
    public Dialogue Dialogue;
    [Space]
    protected Transform Holder;
    protected Text Label;

    // Use this for initialization
    protected virtual void Start()
    {
        Holder = transform.GetChild(0);
        Label = Holder.GetComponentInChildren<Text>();
    }

    //Starts a specific dialogue with a dialogue object
    public void StartDialogue(Dialogue d)
    {
        Dialogue = d;
        StartCoroutine(_Writing());
    }

    protected virtual IEnumerator _Writing()
    {
        Clear();
        Holder.gameObject.SetActive(true);
        while (Dialogue.Writing)
        {
            Write();
            yield return new WaitForSeconds(0.025f);
        }

        FinishWriting();
        while (Dialogue.Active)
        {
            yield return null;
        }

        Holder.gameObject.SetActive(false);
    }

    //Adds a letter to the label
    protected void Write()
    {
        //if there's letters that haven't been written
        if (Label.text.Length < Dialogue.Text.Length)
        {
            Label.text = Dialogue.Text.Substring(0, Label.text.Length + 1);
        }
        else
        {
            Dialogue.Writing = false;
        }
    }

    protected virtual void Clear()
    {
        Dialogue.Active = true;
        Dialogue.Writing = true;

        Label.text = "";
    }

    //Finish the dialogue at once
    public virtual void FinishWriting()
    {
        Label.text = Dialogue.Text;
        Dialogue.Writing = false;
    }
}