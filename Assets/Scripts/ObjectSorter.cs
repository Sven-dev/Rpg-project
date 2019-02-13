using UnityEngine;
using System.Collections;

public class ObjectSorter : MonoBehaviour
{
    new private SpriteRenderer R;

    // Use this for initialization
    void Start ()
    {
        R = GetComponent<SpriteRenderer>();
        SetSortingLayer();
	}

    //Sets the objects sorting layer to be the same as the objects y-coördinate, allowing walking behind other objects
    void SetSortingLayer()
    {
        //adds an amount to the sorting layer relative to the gameobject height
        float objHeight = transform.position.y - R.bounds.size.y / 2.1f;
        R.sortingOrder = (int)(objHeight * -100);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, objHeight, 0), Color.white, 10);
    }
}