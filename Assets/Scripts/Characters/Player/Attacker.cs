using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public bool WindingUp;
    public int WindUpFrames;
    private Movement Movement;

    private void Start()
    {
        Movement = transform.parent.GetComponent<Movement>();
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
        //Make the player move slower
        Movement.Speed = Movement.Speed / 2;

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

        //Set the player speed to normal
        Movement.Speed = Movement.Speed * 2;

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
        Movement.Immobile = true;
        yield return new WaitForSeconds(0.75f);
        Movement.Immobile = false;
    }
}