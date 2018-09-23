using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    protected GameObject Player;

    protected void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public virtual void Interact()
    {
        print("interacted");
    }

}
