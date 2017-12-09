using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorController : Controller {

    public List<GameObject> Doorlist;

    //Toggles the door for each door connected to script
    public override void CheckLogic()
    {
        foreach (GameObject i in Doorlist)
        {
            if (i != null)
            {
                TrialDoor door = i.GetComponent<TrialDoor>();
                door.Toggle();
            }
        }
    }
}
