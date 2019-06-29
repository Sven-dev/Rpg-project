using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : Trigger
{
    public int Scene;
    public int RoomIndex;
    public Vector2 Spawn;

    protected override void ExecuteTrigger()
    {
        Global.SceneSwitcher.SwitchScene(Scene, RoomIndex, Spawn, true, true);
    }
}
