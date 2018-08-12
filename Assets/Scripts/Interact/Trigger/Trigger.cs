using UnityEngine;
using System.Collections;

//Base class for executing a trigger. Abstract: do not put on objects
public abstract class Trigger : MonoBehaviour
{
    protected GameObject Player;

    protected void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    //Base method for starting a trigger.
    public virtual void ExecuteTrigger() { }

    public virtual void ExitTrigger() { }
}
