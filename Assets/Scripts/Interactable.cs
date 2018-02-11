using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

    protected Player p;

    private void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public abstract void Interact();

}
