using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool temp;

    [Range(0, 100)]
    public int Health; //A number between 0 & 100

    public delegate void ValueChange(int value);
    public event ValueChange OnValueChange;

    private void Update()
    {
        if (temp == true)
        {
            OnValueChange(Health);
            temp = false;
        }
    }

    public void ChangeHealth(int value)
    {
        Health += value;

        if (OnValueChange != null)
        {
            OnValueChange(Health);
        }
    }
}