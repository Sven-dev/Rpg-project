using UnityEngine;
using System.Collections;

//Base class for executing a trigger. Abstract: do not put on objects
abstract class Trigger : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}

    //Base method for starting a trigger.
    public abstract void ExecuteTrigger();
}
