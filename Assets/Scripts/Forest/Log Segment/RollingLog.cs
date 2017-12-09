using UnityEngine;
using System.Collections;

public class RollingLog : MonoBehaviour {

    public float Speed;
    public Vector3 Direction;

    private SpriteRenderer renderer;
    private Animator anim;
    private bool pitHit;
    private LogGrate Grate;

    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetSortingLayer();

        anim = GetComponent<Animator>();
        pitHit = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (!pitHit)
        {
            Roll();
        }
        else
        {
            Fall();
        }
	}

    //Makes the Log roll
    void Roll()
    {
        /*
            removed the roll play function, because it had random errors
        */
        transform.Translate(Direction * Speed * Time.deltaTime);
        SetSortingLayer();
    }
   
    //Make the log fade out if it's hit a pit
    void AddTransparency()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Stop"))
        {
            anim.Play("Stop");
            Destroy(GetComponent<Collider>());
        }

        Color tmp = renderer.color;
        tmp.a -= 0.02f;

        if (tmp.a <= 0f)
        {
            Destroy(gameObject);
        }

        renderer.color = tmp;
    }

    //Stops the log animation, and makes the log smaller gradually, giving it the illusion of falling
    void Fall()
    {
        if (GetComponent<Collider>() != null)
        {
            Destroy(GetComponent<Collider>());
        }

        anim.speed = 0;

        transform.localScale -= new Vector3(0.5f, 0.5f, 0) * Time.deltaTime;

        if (transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
        }

        renderer.sortingOrder = (int)(transform.position.y * -100 - transform.localScale.x * -100 - 1000);
    }

    //Checks the direction, sets the correct animationcycle, and returns a vector3
    public void SetVariables(string direction, float speed)
    {
        Speed = speed;
        if (direction == "W")
        {
            //Set animation to Up
            Direction =  Vector3.up;
        }
        else if (direction == "A")
        {
            //Set animation to Left
            Direction =  Vector3.left;
        }
        else if (direction == "S")
        {
            //Set animation to Down
            Direction = Vector3.down;
        }
        else if (direction == "D")
        {
            //Set animation to Right
            Direction = Vector3.right;
        }
        else
        {
            Direction = Vector3.down;
        }
    }

    //Sets the objects sorting layer to be the same as the objects y-coördinate, allowing walking behind other objects
    void SetSortingLayer()
    {
        float objHeight = renderer.bounds.size.y * 0.4f;
        renderer.sortingOrder = (int)((transform.position.y - objHeight) * -10);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Pit")
        {
            pitHit = true;
        }
    }
}
