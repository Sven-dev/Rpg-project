using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float Speed;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetSortingLayer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Makes the player move
    public void Move(Vector3 direction)
    {
        transform.position += (direction * Speed) * Time.fixedDeltaTime;
        SetSortingLayer();
    }

    public void MoveTo(Vector2 target)
    {
        Debug.Log("Times moved");
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
