using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    private Renderer R;

    private void Start()
    {
        R = GetComponentInChildren<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            HealthManager health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                //health.OnValueChange(Damage);
            }
        }
    }

    //Sets the render-order, relative to the other objects in the scene,
    //making it look like the object walks behind other objects
    void SetSortingLayer()
    {
        float objHeight = transform.position.y - R.bounds.size.y / 2f;
        R.sortingOrder = (int)(objHeight * -100);
    }
}