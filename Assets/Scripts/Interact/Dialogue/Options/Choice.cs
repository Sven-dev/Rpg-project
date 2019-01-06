using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Choice : Dialogue
{
    public List<string> ChoiceList;
    [HideInInspector]
    public int SelectedChoice;
}
