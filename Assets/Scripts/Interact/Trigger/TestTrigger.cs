using UnityEngine;
using System.Collections;

class TestTrigger : Trigger
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ExecuteTrigger()
    {
        Debug.Log("overridden trigger");
    }
}
