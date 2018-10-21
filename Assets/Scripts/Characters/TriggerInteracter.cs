using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteracter : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Trigger T = other.GetComponent<Trigger>();
        if (T != null)
        {
            T.ExecuteTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Trigger T = other.GetComponent<Trigger>();
        if (T != null)
        {
            T.ExitTrigger();
        }
    }
}
