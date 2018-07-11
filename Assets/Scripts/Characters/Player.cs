using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    #region Fields
    public bool Balancing;
    private string Direction;
    private Vector3 Deltaposition;
    public bool ControlsLocked;

    private Movement mover;
    #endregion

    void Start()
    {
        //Makes sure the object doesn't unload when switching scenes
        DontDestroyOnLoad(transform.gameObject);

        mover = GetComponent<Movement>();

        //Unlocks the controls (they never ""should"" be, but just in case)
        ControlsLocked = false;
	}

    #region Trigger detection
    //Checsks if the player is standing on a trigger, and activates the trigger
    void OnTriggerEnter(Collider other)
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
    #endregion


}
