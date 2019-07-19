using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DoorController : Controller
{
    [HideInInspector]
    public Button CurrentButton;
    private Button LastButton;

    private void Start()
    {
        //foreach button in scene, check if enabled, if so, put it as lastbutton
        List<GameObject> Buttons = GameObject.FindGameObjectsWithTag("Button").ToList();
        foreach (GameObject G in Buttons)
        {
            Button B = G.GetComponent<Button>();
            if (B.State == true)
            {
                //Only checks for 1 object, since only 1 button should be pressed
                LastButton = B;
                break;
            }
        }
    }

    //Toggles the door for each door connected to script
    public override void CheckLogic()
    {
        if (LastButton != null)
        {
            UpdateDoors();
        }
        else
        {
            ToggleDoors();
        }
    }

    void UpdateDoors()
    {
        List<TrialDoor> AllDoors = new List<TrialDoor>();

        foreach (TrialDoor Door in LastButton.DoorList)
        {
            Door.Opened = false;
            AllDoors.Add(Door);
        }

        foreach (TrialDoor Door in CurrentButton.DoorList)
        {
            Door.Opened = true;
            AllDoors.Add(Door);
        }

        LastButton.TurnOff();
        UpdateCables();
        LastButton = CurrentButton;
    }

    void UpdateCables()
    {
        foreach (Transform CableBox in CurrentButton.transform)
        {
            foreach (Transform Cable in CableBox.transform)
            {
                Cable.GetComponent<CableManager>().TurnON();
            }
        }

        foreach (Transform CableBox in LastButton.transform)
        {
            foreach (Transform Cable in CableBox.transform)
            {
                Cable.GetComponent<CableManager>().TurnOFF();
            }
        }
    }

    void ToggleCables()
    {
        foreach (Transform CableBox in CurrentButton.transform)
        {
            foreach (Transform Cable in CableBox.transform)
            {
                Cable.GetComponent<CableManager>().TurnON();
            }
        }
    }

    void ToggleDoors()
    {
        foreach(TrialDoor Door in CurrentButton.DoorList)
        {
            if (Door.Opened != true)
            {
                Door.Open();
            }
        }

        ToggleCables();
        LastButton = CurrentButton;
    }
}