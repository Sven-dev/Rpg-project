using UnityEngine;
using System.Collections;

//Base class for executing a trigger. Abstract: do not put on objects
public abstract class Trigger : MonoBehaviour
{
    protected Player p;

    protected void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    //Base method for starting a trigger.
    public virtual void ExecuteTrigger() { }

    public virtual void ExitTrigger() { }
}
