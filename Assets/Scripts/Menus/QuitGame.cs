using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MenuItem
{
    public override void SelectItem()
    {
        Application.Quit();
    }
}
