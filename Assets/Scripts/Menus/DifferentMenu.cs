using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifferentMenu : MenuItem
{
    public KeyboardControllerMenu Controller;
    public MenuIndexer Target;

    public override void SelectItem()
    {
        Controller.ActiveMenu.ToggleMenu();
        Controller.ActiveMenu = Target;
        Target.ToggleMenu();
    }
}