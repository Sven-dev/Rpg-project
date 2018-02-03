using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float Speed;
    private Player p;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start ()
    {
        p = GetComponent<Player>();
        renderer = GetComponent<SpriteRenderer>();
        SetSortingLayer();
    }

    //Makes the player move
    public void Move(Vector3 direction)
    {
        Vector3 pos = transform.position + Vector3.down;
        bool walk = false;

        RaycastHit hit;
        if (Physics.Raycast(pos, direction, out hit, 0.5f))
        {
            if (hit.transform.tag == "Player")
            {
                walk = true;
            }
        }
        else
        {
            walk = true;
        }

        if (walk)
        {
            float speedMultiplier = 1f;
            if (p.Balancing)
            {
                speedMultiplier = 0.35f;
            }

            transform.position += direction * Speed * speedMultiplier * Time.fixedDeltaTime;
            SetSortingLayer();
        }
    }

    public void MoveTo(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.fixedDeltaTime);
        SetSortingLayer();
    }

    //Sets the objects sorting layer to be the same as the objects y-coördinate, allowing walking behind other objects
    void SetSortingLayer()
    {
        float objHeight = renderer.bounds.size.y * 0.8f;

        renderer.sortingOrder = (int)((transform.position.y - objHeight) * -10);
    }
}
