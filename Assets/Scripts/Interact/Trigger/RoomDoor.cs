using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomDoor : Trigger
{
    public GameObject Room;
    public RoomDoor Destination;
    [HideInInspector]
    public Transform Spawn;

    private void Start()
    {
        Spawn = transform.GetChild(0);
    }

    protected override void ExecuteTrigger()
    {
        if (Destination != null)
        {
            Global.SceneSwitcher.SwitchRoom(Room, Destination.Spawn.position, true, true);
        }
    }
}