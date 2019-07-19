using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentScene : MenuItem
{
    public SceneSwitcherMenu SceneSwitcher;

    public override void SelectItem()
    {
        SceneSwitcher.SwitchScene();
    }
}
