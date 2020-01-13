using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public int Damage;

    private Renderer Renderer;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
        SetSortingLayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager h = collision.GetComponent<HealthManager>();
        if (h != null)
        {
            h.ChangeHealth(Damage);
        }

        Destroy(gameObject);
    }

    //Sets the render-order, relative to the other objects in the scene,
    //making it look like the object walks behind other objects
    void SetSortingLayer()
    {
        float objHeight = transform.position.y - Renderer.bounds.size.y / 2f;
        Renderer.sortingOrder = (int)(objHeight * -100);
    }
}