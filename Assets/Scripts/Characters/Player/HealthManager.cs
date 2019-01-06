using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthBar Healthbar;

    private int Health; //A number between 0 & 100
    [Range(0, 100)]
    public int health; //temp

    public delegate void HealthChange(int health);
    public event HealthChange OnHealthChange;

    // Use this for initialization
    void Start ()
    {
        //needs to take health out of save file
        Health = 100;
        Healthbar.Link(this);
	}
	
	//temp
	void Update ()
    {
		if (health != Health)
        {
            Health = health;
            OnHealthChange(Health);
        }
	}

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            //game over
        }
        else
        {
            OnHealthChange(Health);
        }
    }
}
