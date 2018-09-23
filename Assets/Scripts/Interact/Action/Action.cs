using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public bool Active = false;

    public virtual void StartProcess()
    {
        print("Base action" + name);
    }

    public virtual void StartProcess(GameObject target)
    {
        print("Base action" + name);
    }
}
