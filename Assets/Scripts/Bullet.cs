using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager health = collision.gameObject.GetComponent<HealthManager>();
        if (health != null)
        {
            health.TakeDamage(Damage);
        }
    }
}