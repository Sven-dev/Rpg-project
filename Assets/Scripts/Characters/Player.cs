using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    #region Fields
    private string Direction;
    private bool ControlsLocked;

    private Animator anim;
    private Movement mover;
    #endregion

    #region Unity Logic
    void Start()
    {
        //Makes sure the object doesn't unload when switching scenes
        DontDestroyOnLoad(transform.gameObject);

        mover = GetComponent<Movement>();

        //Gets the animator and sets a default animation and direction
        anim = GetComponent<Animator>();
        anim.Play("D_Idle");
        Direction = "D";

        //Unlocks the controls (they never ""should"" be, but just in case)
        ControlsLocked = false;
	}

    void Update()
    {
        CheckForInteract();
    }

    void FixedUpdate()
    {
        CheckForMovement();
    }
    #endregion

    #region Movement
    void CheckForMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!ControlsLocked)
            {
                //move up (W)
                if (Input.GetKey(KeyCode.W))
                {
                    Direction = "W";
                    mover.Move(GetDirectionVector());
                }

                //move down (S)
                if (Input.GetKey(KeyCode.S))
                {
                    Direction = "S";
                    mover.Move(GetDirectionVector());
                }

                //move left (A)
                if (Input.GetKey(KeyCode.A))
                {
                    Direction = "A";
                    mover.Move(GetDirectionVector());
                }

                //move right (D)
                if (Input.GetKey(KeyCode.D))
                {
                    Direction = "D";
                    mover.Move(GetDirectionVector());
                }

                anim.Play(Direction + "_Walk");
            }
        }

        else //idle
        {
            anim.Play(Direction + "_Idle");
        }
    }
    #endregion

    #region Interacting
    public string GetDirection()
    {
        return Direction;
    }

    void CheckForInteract()
    {
        //Checks if z or enter is pressed (interaction)
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!ControlsLocked)
            {
                InteractWithObject();
            }
        }
    }

    //Uses a raycast to check in front of the player for an interactable object, and interacts with it
    void InteractWithObject()
    {
        Vector3 pos = transform.position;
        pos += Vector3.down;

        Vector3 dir = GetDirectionVector();
        float dis = 1f;

        RaycastHit hit;
        if (Physics.Raycast(pos, dir, out hit, dis))
        {
            Interactable Object = hit.transform.gameObject.GetComponent<Interactable>();
            if (Object != null)
            {
                Controls_OFF();
                Object.Interact();
            }
        }
    }
    #endregion

    #region Trigger detection
    //Checsks if the player is standing on a trigger, and activates the trigger
    void OnTriggerEnter(Collider other)
    {
        Trigger T = other.GetComponent<Trigger>();

        if (T != null)
        {
            Controls_OFF();
            T.ExecuteTrigger();
        }
    }
    #endregion

    #region Toggle Movement
    //Locks the player's controls in case they're in a cutscene or in dialogue
    public void ControlsToggle()
    {
        ControlsLocked = !ControlsLocked;
    }
    public void Controls_OFF()
    {
        ControlsLocked = true;
    }

    public void Controls_ON()
    {
        ControlsLocked = false;
    }
    #endregion

    #region Misc.

    //Takes a direction (W, A, S, D) and returns a vector3
    Vector3 GetDirectionVector()
    {
        if(Direction == "W")
        {
            return Vector3.up;
        }
        else if (Direction == "A")
        {
            return Vector3.left;
        }
        else if (Direction == "S")
        {
            return Vector3.down;
        }
        else if (Direction == "D")
        {
            return Vector3.right;
        }
        else
        {
            throw new System.Exception();
        }
    }
    #endregion
}
