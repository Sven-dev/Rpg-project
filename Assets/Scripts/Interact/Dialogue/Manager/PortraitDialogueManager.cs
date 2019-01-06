using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitDialogueManager : DialogueManager
{
    private Image Portrait;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Portrait = Holder.GetComponentInChildren<Image>();
    }
}