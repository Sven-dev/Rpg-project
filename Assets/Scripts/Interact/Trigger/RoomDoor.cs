using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomDoor : Trigger
{
    public GameObject Room;
    private Transform Spawn;

    private void Start()
    {
        Spawn = transform.GetChild(0);
    }

    protected override void ExecuteTrigger()
    {
        print("door: " + name);
        Global.SceneSwitcher.SwitchRoom(Room, Spawn.position, true, true);
    }
}