using UnityEngine;
using System.Collections;

//Base class for executing a trigger.
public abstract class Trigger : MonoBehaviour
{
    //Base method for starting a trigger
    protected virtual void ExecuteTrigger() { }

    protected virtual void ExitTrigger() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExecuteTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExitTrigger();
        }
    }
}