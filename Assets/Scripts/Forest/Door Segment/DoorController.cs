using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DoorController : Controller
{
    private Button LastButton;
    public Button CurrentButton;

    public List<TrialDoor> OpenedDoors;

    //Toggles the door for each door connected to script
    public override void CheckLogic()
    {
        //List of all doors that need to stay opened

        //for each door connected to button 1, 
        //if the door is also connected to button 2, it needs to stay open
        if (LastButton != null)
        {
            foreach (TrialDoor Door in CurrentButton.DoorList.Intersect(LastButton.DoorList))
            {
                OpenedDoors.Add(Door);
            }

            LogicDoors(OpenedDoors);
        }
        else
        {
            ToggleDoors();
        }
    }

    void LogicDoors(List<TrialDoor> OpenedDoors)
    {
        foreach(TrialDoor Door in LastButton.DoorList)
        {
            Door.Close();
        }

        foreach(TrialDoor Door in CurrentButton.DoorList)
        {
            Door.Open();
        }

        /*
        //Closes every door connected to LastButton, except if they are in OpenedDoors (it needs to stay open)
        var differences = LastButton.DoorList.Except(OpenedDoors);
        foreach (var difference in differences)
        {
            TrialDoor door = difference as TrialDoor;
            door.Close();
        }

        //Opens every door connected to CurrentButton, except if they are in OpenedDoors (it is already open)
        foreach (TrialDoor Door in CurrentButton.DoorList)
        {
            if (Door.State == false)
            {
                Door.Open();
            }
        }
        */

        LastButton.TurnOff();
        LastButton = CurrentButton;
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