using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteracter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
