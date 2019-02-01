using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int _health; //A number between 0 & 100
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                print("Game over");
            }
            else
            {
                if (OnHealthChange != null)
                {
                    OnHealthChange(Health);
                }
            }
        }
    }

    public delegate void HealthChange(int health);
    public event HealthChange OnHealthChange;

    // Use this for initialization
    void Start()
    {
         Health = GlobalVariables.Save.PlayerHealth;       
	}

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}