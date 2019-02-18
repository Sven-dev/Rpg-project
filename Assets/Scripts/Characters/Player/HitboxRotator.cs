using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates the object based on the direction the parent is looking
public class HitboxRotator : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        transform.parent.GetComponent<Movement>().OnMovementChange += Rotate;
	}

    //Rotates the object so it faces the way the player is looking
    private void Rotate(Direction d)
    {
        switch (Global.PlayerMovement.Direction)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
}
