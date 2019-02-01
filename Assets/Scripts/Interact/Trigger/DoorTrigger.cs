﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : Trigger
{
    public string Scene;
    public Vector2 position;

    public override void ExecuteTrigger()
    {
        GlobalVariables.SceneSwitcher.SwitchInOut(Scene, position);
    }
}