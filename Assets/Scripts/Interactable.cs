using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    protected GameObject Player;

    protected virtual void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public abstract void Interact();
}