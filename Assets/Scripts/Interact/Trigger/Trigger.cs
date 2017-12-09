using UnityEngine;
using System.Collections;

//Base class for executing a trigger. Abstract: do not put on objects
public abstract class Trigger : MonoBehaviour
{
    //Base method for starting a trigger.
    public abstract void ExecuteTrigger();
}
