using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public bool Active = false;

    public abstract void StartProcess();
}
