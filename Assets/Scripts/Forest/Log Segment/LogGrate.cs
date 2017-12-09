using UnityEngine;
using System.Collections;

public class LogGrate : MonoBehaviour
{
    public Sprite Opened;
    public Sprite Closed;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        Close();
	}

    //raycasts above the grate to check for an object (character or log)
    void CheckForObject(Collider hit)
    {
        //Checks if the grate is open, if not, opens it
        if (sr.sprite != Opened)
        {
            Open();
        }

        /*
        //if the object is a log
        if (hit.transform.tag == "Log")
        {
            hit.transform.gameObject.GetComponent<RollingLog>().HitGrate(this);
        }
        */

        //if the object is an npc or the player
        else if (hit.transform.tag == "Character" || hit.transform.tag == "Player")
        {
            //To Do: Character.fall();
        }
        else
        {
            if (sr.sprite == Closed)
            {
                Close();
            }
        }
    }

    //Opens the grate
    void Open()
    {
        sr.sprite = Opened;
    }

    //Closes the grate
    public void Close()
    {
        sr.sprite = Closed;
    }

    void OnTriggerEnter(Collider other)
    {
        CheckForObject(other);
    }

    void OnTriggerExit(Collider other)
    {
        Close();
    }
}