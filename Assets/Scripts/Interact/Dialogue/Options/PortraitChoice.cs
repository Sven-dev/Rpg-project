using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PortraitChoice : Dialogue
{
    public Image Picture;
    public List<string> ChoiceList;
    [HideInInspector]
    public int SelectedChoice;
}