using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DoorController : Controller
{
    private Button LastButton;
    public Button CurrentButton;

    //Toggles the door for each door connected to script
    public override void CheckLogic()
    {
        if (LastButton != null)
        {
            List<TrialDoor> Doors = new List<TrialDoor>();

            foreach (TrialDoor Door in LastButton.DoorList)
            {
                Door.State = false;
                Doors.Add(Door);
            }
            foreach (TrialDoor Door in CurrentButton.DoorList)
            {
                Door.State = true;
                Doors.Add(Door);
            }

            List<TrialDoor> DistDoors = Doors.Distinct().ToList();

            foreach (TrialDoor Door in DistDoors)
            {
                Door.UpdateSprite();
            }

            LastButton.TurnOff();
            LastButton = CurrentButton;
        }
        else
        {
            ToggleDoors();
        }
    }

    void ToggleDoors()
    {
        foreach(TrialDoor Door in CurrentButton.DoorList)
        {
            if (Door.State != true)
            {
                Door.Open();
            }
        }

        LastButton = CurrentButton;
    }
}