using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public bool State;

    public Sprite ON;
    public Sprite OFF;

    private SpriteRenderer S;

    private void Start()
    {
        S = GetComponent<SpriteRenderer>();
    }

    public void Toggle()
    {
        State = !State;
        UpdateSprite();
    }

    public void TurnON()
    {
        State = true;
        UpdateSprite();
    }

    public void TurnOFF()
    {
        State = false;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (State == true)
        {
            S.sprite = ON;
        }
        else //if (state == false)
        {
            S.sprite = OFF;
        }
    }
}
