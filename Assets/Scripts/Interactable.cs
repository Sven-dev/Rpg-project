using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    protected Player p;

    protected void Start()
    {
        p = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public virtual void Interact()
    {
        print("interacted");
    }

}
