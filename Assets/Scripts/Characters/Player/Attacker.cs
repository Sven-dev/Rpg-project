using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public bool WindingUp;
    public int WindUpFrames;

    private void Start()
    {
        Global.PlayerMovement.OnMovementChange += Rotate;
    }

    //Cancels the wind-up
    public void LetGo()
    {
        WindingUp = false;
    }

    //Wind up the attack
    public void WindUp()
    {
        WindingUp = true;
        StartCoroutine(_WindUp());
    }

    private IEnumerator _WindUp()
    {
        //While the attack is winding up
        int frames = 0;
        bool WoundUp = false;
        while (WindingUp)
        {
            print("Winding up");
            //If the player lets the attack go
            if (WindingUp == false)
            {
                break;
            }

            //Up the windup frames
            frames++;
            //If the attack is wound up long enough, set it to ready
            if (frames == WindUpFrames)
            {
                print("Completely wound up");
                WoundUp = true;
            }
            yield return new WaitForSeconds(0.01f);
        }

        //If the attack is ready, attack
        if (WoundUp)
        {
            Attack();
        }
    }

    //Launch the attack
    private void Attack()
    {
        StartCoroutine(_Attack());
    }

    private IEnumerator _Attack()
    {
        print("Attack");
        yield return null;
    }

    //Rotates the object so it faces the way the player is looking
    private void Rotate()
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