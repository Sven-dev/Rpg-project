using UnityEngine;
using System.Collections;

public class CombatPlayer : MonoBehaviour {

    #region Fields
    public float Speed;
    private string Direction;
    private bool ControlsLocked;

    private Animator anim;
    private SpriteRenderer renderer;
    #endregion

    #region Unity Logic
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        ControlsLocked = false;
    }
	
	// Update is called once per frame
	void Update()
    {
	    
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
                //move right (W)
                if (Input.GetKey(KeyCode.W))
                {
                    Direction = "W";
                    Move(GetDirectionVector());
                }

                //move left (S)
                if (Input.GetKey(KeyCode.S))
                {
                    Direction = "S";
                    Move(GetDirectionVector());
                }

                //move left (A)
                if (Input.GetKey(KeyCode.A))
                {
                    Direction = "A";
                    Move(GetDirectionVector());
                }

                //move right (D)
                if (Input.GetKey(KeyCode.D))
                {
                    Direction = "D";
                    Move(GetDirectionVector());
                }
            }
        }
    }

    //Makes the player move
    void Move(Vector3 direction)
    {
        transform.position += (direction * Speed) * Time.fixedDeltaTime;
    }

    //Takes a direction (W, A, S, D) and returns a vector3
    Vector3 GetDirectionVector()
    {
        if (Direction == "W")
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
